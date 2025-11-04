// Restaurant.Api/Filters/GlobalExceptionFilter.cs
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Restaurant.Api.Core.Exceptions;

public class GlobalExceptionFilter : IExceptionFilter
{
    private readonly IWebHostEnvironment _env;
    private readonly ILogger<GlobalExceptionFilter> _logger;

    public GlobalExceptionFilter(IWebHostEnvironment env, ILogger<GlobalExceptionFilter> logger)
    {
        _env = env;
        _logger = logger;
    }

    public void OnException(ExceptionContext context)
    {
        _logger.LogError(context.Exception, "Error en la aplicación");

        var response = context.Exception switch
        {
            EntityNotFoundException ex => new ErrorResponse
            {
                StatusCode = StatusCodes.Status404NotFound,
                Message = ex.Message
            },
            DatabaseConnectionException => new ErrorResponse
            {
                StatusCode = StatusCodes.Status503ServiceUnavailable,
                Message = "Servicio no disponible temporalmente"
            },
            InfrastructureException => new ErrorResponse
            {
                StatusCode = StatusCodes.Status500InternalServerError,
                Message = context.Exception.Message
            },
            DomainValidationException ex => new ErrorResponse
            {
                StatusCode = (int)HttpStatusCode.BadRequest,
                Message = "Error de validación",
                Errors = ex.Errors.ToDictionary(x => x.Key, x => x.Value)
            },
            UnauthorizedException ex => new ErrorResponse
            {
                StatusCode = (int)HttpStatusCode.Unauthorized,
                Message = ex.Message
            },
            AppException ex => new ErrorResponse
            {
                StatusCode = (int)HttpStatusCode.BadRequest,
                Message = ex.Message
            },
            _ => new ErrorResponse
            {
                StatusCode = StatusCodes.Status500InternalServerError,
                Message = _env.IsDevelopment() 
                    ? context.Exception.Message 
                    : "Ocurrió un error en el servidor"
            },
        };

        context.Result = new ObjectResult(response) 
        { 
            StatusCode = response.StatusCode 
        };
        context.ExceptionHandled = true;
    }
}

public class ErrorResponse
{
    public int StatusCode { get; set; }
    public string Message { get; set; } = string.Empty;
    public string StackTrace { get; set; } = string.Empty;
    public Dictionary<string, string[]> Errors { get; set; } = new Dictionary<string, string[]>();
}