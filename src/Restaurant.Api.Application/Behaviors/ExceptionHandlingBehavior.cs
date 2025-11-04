// Restaurant.Api.Application/Behaviors/ExceptionHandlingBehavior.cs
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurant.Api.Core.Exceptions;
using System.Net;

public class ExceptionHandlingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly ILogger<ExceptionHandlingBehavior<TRequest, TResponse>> _logger;

    public ExceptionHandlingBehavior(ILogger<ExceptionHandlingBehavior<TRequest, TResponse>> logger)
    {
        _logger = logger;
    }

    public async Task<TResponse> Handle(
        TRequest request, 
        RequestHandlerDelegate<TResponse> next, 
        CancellationToken cancellationToken)
    {
        try
        {
            return await next();
        }
        catch (FluentValidation.ValidationException ex)
        {
            _logger.LogWarning(ex, "Error de validación de FluentValidation en {RequestName}", typeof(TRequest).Name);
            var errors = ex.Errors
                .GroupBy(x => x.PropertyName)
                .ToDictionary(
                    g => g.Key,
                    g => g.Select(x => x.ErrorMessage).ToArray()
                );
            throw new DomainValidationException(errors);
        }
        catch (AppException ex)
        {
            _logger.LogWarning(ex, "Error de aplicación en {RequestName}: {Message}", 
                typeof(TRequest).Name, ex.Message);
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error inesperado al procesar {RequestName}", typeof(TRequest).Name);
            throw new AppException("Ocurrió un error inesperado al procesar la solicitud", ex);
        }
    }
}