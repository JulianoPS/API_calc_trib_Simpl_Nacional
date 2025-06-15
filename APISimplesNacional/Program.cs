using APISimplesNacional.Application.Interfaces;
using APISimplesNacional.Application.Mapping;
using APISimplesNacional.Application.Services;
using APISimplesNacional.Application.Validators;
using APISimplesNacional.Domain.Interfaces;
using APISimplesNacional.Domain.Repositories;
using APISimplesNacional.Infra.Contexto;
using APISimplesNacional.Infra.Repositorios;
using APISimplesNacional.Middlewares;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Reflection;


var builder = WebApplication.CreateBuilder(args);

// Configuração da Connection String (puxa do appsettings.json)
builder.Services.AddDbContext<SimplesNacionalDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IEmpresaService, EmpresaService>();
builder.Services.AddScoped<IErroLogRepositorio, ErroLogRepositorio>();
builder.Services.AddScoped<IErroLogService, ErroLogService>();
builder.Services.AddScoped<ITabelaIRService, TabelaIRService>();
builder.Services.AddScoped<ITabelaIRRepositorio, TabelaIRRepositorio>(); 
builder.Services.AddScoped<IEmpresaRepositorio, EmpresaRepositorio>();
builder.Services.AddScoped<IClonagemRepositorio, ClonagemRepositorio>();
builder.Services.AddScoped<ITabelaINSSRepositorio, TabelaINSSRepositorio>();
builder.Services.AddScoped<ITabelaINSSService, TabelaINSSService>();
builder.Services.AddScoped<IAnexoIIIRepositorio, AnexoIIIRepositorio>();
builder.Services.AddScoped<IAnexoIIIService, AnexoIIIService>();
builder.Services.AddScoped<IAnexoVRepositorio, AnexoVRepositorio>();
builder.Services.AddScoped<IAnexoVService, AnexoVService>();
builder.Services.AddScoped<ICalculoDespesaService, CalculoDespesaService>();
builder.Services.AddScoped<IEmpresaService, EmpresaService>();
builder.Services.AddScoped<IEmpresaRepositorio, EmpresaRepositorio>();
builder.Services.AddScoped<ICalculoInssService, CalculoInssService>();
builder.Services.AddScoped<ICalculoIrService, CalculoIrService>();
builder.Services.AddScoped<IAtividadeService, AtividadeService>();


builder.Services.AddAutoMapper(typeof(EmpresasProfile).Assembly);
builder.Services.AddValidatorsFromAssemblyContaining<CalculoRequestDtoValidator>();

builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining<CalculoRequestDtoValidator>();
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssembly(typeof(EmpresaDtoValidator).Assembly);


// Adiciona controladores
builder.Services.AddControllers();

// Adiciona o Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "API Simples Nacional",
        Version = "v1",
        Description = "Endpoints de cálculo e parametrização de impostos do Simples Nacional."
    });

    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    options.IncludeXmlComments(xmlPath, includeControllerXmlComments: true);
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy =>
        {
            policy
                //.WithOrigins("http://localhost:4200") // 👉 Frontend Angular
                .AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowAnyMethod();
            // .AllowCredentials();
        });
});

var app = builder.Build();

// Auto-aplica migrations ao iniciar
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<SimplesNacionalDbContext>();
    try
    {
        db.Database.Migrate();
    }
    catch (Exception ex)
    {
        var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "Erro ao aplicar migrations no startup.");
        // Se desejar não iniciar em caso de falha:
        throw;
    }
}


// Middleware global de tratamento de exceções
app.UseMiddleware<ExceptionMiddleware>();

app.UseCors("AllowAll");

var apiPrefix = builder.Configuration["ApiPrefix"] ?? string.Empty;
if (!string.IsNullOrEmpty(apiPrefix))
{
    app.UsePathBase(apiPrefix);
    // Em produção, JSON em /{documentName}/swagger.json
    app.UseSwagger(c => c.RouteTemplate = "{documentName}/swagger.json");
    app.UseSwaggerUI(c =>
    {
        c.RoutePrefix = "swagger";
        c.SwaggerEndpoint($"{apiPrefix}/v1/swagger.json", "API v1");
    });
}
else
{
    // Em dev, use padrão:
    app.UseSwagger(); // JSON em /swagger/v1/swagger.json
    app.UseSwaggerUI(c =>
    {
        c.RoutePrefix = string.Empty; // opcional, UI direto na raiz
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1");
    });
}


// HTTPS Redireciona HTTP para HTTPS
app.UseHttpsRedirection();

// Autorização Middleware de autorização
app.UseAuthorization();

// Mapeia os controladores
app.MapControllers();

// Executa a aplicação
app.Run();
