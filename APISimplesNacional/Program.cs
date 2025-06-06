using APISimplesNacional.Application.Interfaces;
using APISimplesNacional.Application.Services;
using APISimplesNacional.Domain.Interfaces;
using APISimplesNacional.Domain.Repositories;
using APISimplesNacional.Infra.Contexto;
using APISimplesNacional.Infra.Repositorios;
using APISimplesNacional.Middlewares;
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

// Middleware global de tratamento de exceções
app.UseMiddleware<ExceptionMiddleware>();

app.UseCors("AllowAll");

// Habilita o Swagger mesmo fora do ambiente de desenvolvimento
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "API Simples Nacional v1");
    c.RoutePrefix = string.Empty; // Faz com que o Swagger abra direto na raiz: https://localhost:7099/
});

// HTTPS Redireciona HTTP para HTTPS
app.UseHttpsRedirection();

// Autorização Middleware de autorização
app.UseAuthorization();

// Mapeia os controladores
app.MapControllers();

// Executa a aplicação
app.Run();
