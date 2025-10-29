

using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

namespace B.MiddleWare;

public class GlobalExceptionMiddleWare
{
    private readonly RequestDelegate next;
    private readonly ILogger<GlobalExceptionMiddleWare> logger;

    public GlobalExceptionMiddleWare(RequestDelegate next, ILogger<GlobalExceptionMiddleWare> logger) 
    {
        this.next = next;
        this.logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            logger.LogError("Eccezione non gestita. Scemo, CORREGGI");
            await HandleException(context, ex);
        }
    }
    private static Task HandleException(HttpContext context, Exception ex)
    {
        var (statusCode, title) = ex switch
        {
            KeyNotFoundException => (StatusCodes.Status404NotFound, "Non trovato"),
            InvalidProgramException => (StatusCodes.Status406NotAcceptable, "Non valida"),
            ValidationException =>(StatusCodes.Status400BadRequest,"BadRequest"),
            _ => (StatusCodes.Status500InternalServerError, "Errore Server")
        };

        var x = Activity.Current?.Id == context.TraceIdentifier;


        var problem = new ProblemDetails()
        {
            Detail = ex.Message,
            Status = statusCode,
            Title = title,
            Instance = context.Request.Path,
            
        };
        problem.Extensions["TRACEID"] = x;

        return context.Response.WriteAsJsonAsync(problem);
    }
}
