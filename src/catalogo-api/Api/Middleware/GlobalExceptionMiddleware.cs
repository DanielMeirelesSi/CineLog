using System.Text.Json;
using CatalogoApi.Api.Dtos.Responses;
using CatalogoApi.Domain.Exceptions;

namespace CatalogoApi.Api.Middleware;

public class GlobalExceptionMiddleware(RequestDelegate next)
{
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (NotFoundException ex)
        {
            await EscreverErro(context, StatusCodes.Status404NotFound, ex.Message);
        }
        catch (ConflictException ex)
        {
            await EscreverErro(context, StatusCodes.Status409Conflict, ex.Message);
        }
        catch (DomainException ex)
        {
            await EscreverErro(context, StatusCodes.Status422UnprocessableEntity, ex.Message);
        }
        catch (Exception)
        {
            await EscreverErro(context, StatusCodes.Status500InternalServerError, "Ocorreu um erro interno no servidor.");
        }
    }

    private static async Task EscreverErro(HttpContext context, int statusCode, string mensagem)
    {
        context.Response.StatusCode = statusCode;
        context.Response.ContentType = "application/json";
        ErroResponse erro = new(mensagem, []);
        string json = JsonSerializer.Serialize(erro, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
        await context.Response.WriteAsync(json);
    }
}
