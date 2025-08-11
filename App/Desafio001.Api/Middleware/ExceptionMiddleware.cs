using Desafio001.Dominio.Exceptions;
using System.Net;
using System.Text.Json;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionMiddleware> _logger;

    public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }
        catch (CustomException ex)
        {
            _logger.LogWarning(ex, "Erro tratado");

            httpContext.Response.ContentType = "application/json";
            httpContext.Response.StatusCode = ex.StatusCode;

            var responseBody = new
            {
                error = true,
                titulo = "PROCEDIMENTO_NAO_CONCLUIDO",
                detalhes = new
                {
                    mensagem = ex.Message,
                    codigo = ex.StatusCode
                }
            };

            var json = JsonSerializer.Serialize(responseBody, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            });

            await httpContext.Response.WriteAsync(json);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro inesperado no servidor");

            httpContext.Response.ContentType = "application/json";
            httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            var responseBody = new
            {
                error = true,
                titulo = "ERRO_INTERNO_SERVIDOR",
                detalhes = new
                {
                    mensagem = "Ocorreu um erro inesperado. Tente novamente mais tarde.",
                    excecao = ex.Message
                }
            };

            var json = JsonSerializer.Serialize(responseBody, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            });

            await httpContext.Response.WriteAsync(json);
        }
    }
}
