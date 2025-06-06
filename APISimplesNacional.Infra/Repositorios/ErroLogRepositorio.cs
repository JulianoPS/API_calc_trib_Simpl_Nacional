using APISimplesNacional.Domain.Repositories;
using APISimplesNacional.Infra.Contexto;
using APISimplesNacional.Infra.Entidades;

namespace APISimplesNacional.Infra.Repositorios
{
    public class ErroLogRepositorio : IErroLogRepositorio
    {
        private readonly SimplesNacionalDbContext _context;

        public ErroLogRepositorio(SimplesNacionalDbContext context)
        {
            _context = context;
        }

        public async Task RegistrarErroAsync(Exception ex, int statusCode)
        {
            var erro = new ErroLog
            {
                Mensagem = ex.Message,
                StackTrace = ex.StackTrace ?? "",
                Origem = ex.Source ?? "",
                StatusCode = statusCode
            };

            await _context.ErrosLog.AddAsync(erro);
            await _context.SaveChangesAsync();
        }
    }
}
