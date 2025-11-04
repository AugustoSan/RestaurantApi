// Restaurant.Api.Infrastructure/Filters/RepositoryExceptionFilter.cs
using MongoDB.Driver;
using Restaurant.Api.Core.Exceptions;

namespace Restaurant.Api.Infrastructure.Filters;

public static class RepositoryExceptionFilter
{
    public static T ExecuteWithExceptionHandling<T>(Func<T> repositoryAction, string entityName, object? key = null)
    {
        try
        {
            return repositoryAction();
        }
        catch (MongoConnectionException ex)
        {
            throw new DatabaseConnectionException("Error de conexión con la base de datos", ex);
        }
        catch (MongoWriteException ex) when (ex.WriteError.Category == ServerErrorCategory.DuplicateKey)
        {
            throw new InfrastructureException($"Ya existe un registro con el mismo identificador: {key}");
        }
        catch (MongoCommandException ex) when (ex.Code == 11000) // Código para duplicados
        {
            throw new InfrastructureException($"Violación de restricción única: {ex.Message}");
        }
        catch (InvalidOperationException ex) when (ex.Message.Contains("Sequence contains no elements"))
        {
            throw new EntityNotFoundException(entityName, key);
        }
        catch (Exception ex)
        {
            throw new InfrastructureException($"Error en la capa de infraestructura: {ex.Message}", ex);
        }
    }

    public static async Task<T> ExecuteWithExceptionHandlingAsync<T>(
        Func<Task<T>> repositoryAction, 
        string entityName, 
        object? key = null)
    {
        try
        {
            return await repositoryAction();
        }
        catch (MongoConnectionException ex)
        {
            throw new DatabaseConnectionException("Error de conexión con la base de datos", ex);
        }
        catch (MongoWriteException ex) when (ex.WriteError.Category == ServerErrorCategory.DuplicateKey)
        {
            throw new InfrastructureException($"Ya existe un registro con el mismo identificador: {key}");
        }
        catch (MongoCommandException ex) when (ex.Code == 11000)
        {
            throw new InfrastructureException($"Violación de restricción única: {ex.Message}");
        }
        catch (InvalidOperationException ex) when (ex.Message.Contains("Sequence contains no elements"))
        {
            throw new EntityNotFoundException(entityName, key);
        }
        catch (Exception ex)
        {
            throw new InfrastructureException($"Error en la capa de infraestructura: {ex.Message}", ex);
        }
    }
}