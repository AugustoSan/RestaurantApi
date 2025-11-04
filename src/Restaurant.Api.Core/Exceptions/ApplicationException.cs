// Restaurant.Api.Core/Exceptions/ApplicationException.cs
namespace Restaurant.Api.Core.Exceptions;

// Cambiar el nombre a AppException
public class AppException : Exception
{
    public AppException(string message) : base(message) { }
    public AppException(string message, Exception innerException) 
        : base(message, innerException) { }
}

public class DomainValidationException : AppException
{
    public IDictionary<string, string[]> Errors { get; }

    public DomainValidationException(IDictionary<string, string[]> errors) 
        : base("Uno o más errores de validación han ocurrido")
    {
        Errors = errors;
    }
}

public class UnauthorizedException : AppException
{
    public UnauthorizedException(string message) : base(message) { }
}