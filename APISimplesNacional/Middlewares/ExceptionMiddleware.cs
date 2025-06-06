using APISimplesNacional.Application.Interfaces;

namespace APISimplesNacional.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                var statusCode = 500;
                context.Response.StatusCode = statusCode;
                context.Response.ContentType = "application/json";

                // Resolve o IErroLogService manualmente no escopo da requisição
                var erroLogService = context.RequestServices.GetService<IErroLogService>();

                if (erroLogService != null)
                {
                    await erroLogService.RegistrarErroAsync(ex, statusCode);
                }

                var response = new
                {
                    status = statusCode,
                    erro = "Ocorreu um erro interno no servidor. A equipe técnica foi notificada."
                };

                var json = System.Text.Json.JsonSerializer.Serialize(response);
                await context.Response.WriteAsync(json);
            }
        }
    }
}