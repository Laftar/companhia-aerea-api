using Desafio001.Api.Validadores;
using Desafio001.Api.ViewModels.Exemplos;
using Desafio001.Dominio.Configuracoes;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using SharpGrip.FluentValidation.AutoValidation.Mvc.Extensions;
using Swashbuckle.AspNetCore.Filters;

var builder = WebApplication.CreateBuilder(args);

var connStr = builder.Configuration.GetConnectionString("pessoal");
if (connStr is not null) builder.Services.AdicionarApp(connStr);

builder.Services.Configure<MensageriaConfig>(builder.Configuration.GetSection("RabbitMQ"));

builder.Services.AddValidatorsFromAssemblyContaining<CriarPilotoValidador>();
builder.Services.AddFluentValidationAutoValidation();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.EnableAnnotations();
    c.ExampleFilters();
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "Desafio001 API",
        Version = "v1"
    });
});

builder.Services.AddSwaggerExamplesFromAssemblyOf<CriarPilotoExemplo>();

builder.Services.AddCors(opt =>
{
    opt.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
    });
});

var app = builder.Build();

app.UseMiddleware<ExceptionMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Desafio001 API V1");
        c.RoutePrefix = string.Empty;
    });
}

app.UseCors("AllowAll");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();