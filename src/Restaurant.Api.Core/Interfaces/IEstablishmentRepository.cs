using Restaurant.Api.Core.Entities;

namespace Restaurant.Api.Core.Interfaces;

public interface IEstablishmentRepository {
    Task Update(Establishment restaurant);
    Task<Establishment?> GetInfo();
}
