// Restaurant.Api.Core/Exceptions/InfrastructureException.cs
namespace Restaurant.Api.Core.Exceptions;

public class InfrastructureException : Exception
{
    public InfrastructureException(string message) : base(message) { }
    public InfrastructureException(string message, Exception innerException) 
        : base(message, innerException) { }
}

public class DatabaseConnectionException : InfrastructureException
{
    public DatabaseConnectionException(string message) : base(message) { }
    public DatabaseConnectionException(string message, Exception innerException) 
        : base(message, innerException) { }
}

public class EntityNotFoundException : InfrastructureException
{
    public string EntityName { get; }
    public object? Key { get; }

    public EntityNotFoundException(string entityName, object? key) 
        : base(key != null 
            ? $"No se encontró la entidad '{entityName}' con el ID: {key}"
            : $"No se encontró la entidad '{entityName}'")
    {
        EntityName = entityName;
        Key = key;
    }
}