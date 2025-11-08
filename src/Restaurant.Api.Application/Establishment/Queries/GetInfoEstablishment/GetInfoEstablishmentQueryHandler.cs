using MediatR;
using Restaurant.Api.Application.Establishment.Queries.GetInfoEstablishment;
using Restaurant.Api.Application.Common.Models;
using Restaurant.Api.Application.Establishment.Dtos;
using Restaurant.Api.Core.Interfaces;
using Restaurant.Api.Application.Establishment.Mapper;

namespace Restaurant.Api.Application.Establishment.Queries.GetInfoEstablishment;

public class GetInfoEstablishmentQueryHandler(
    IEstablishmentRepository establishmentRepository
) : IRequestHandler<GetInfoEstablishmentQuery, EstablishmentDto?>
{
    private readonly IEstablishmentRepository _establishmentRepository = establishmentRepository;
    public async Task<EstablishmentDto?> Handle(GetInfoEstablishmentQuery request, CancellationToken cancellationToken)
    {
        var establishment = await _establishmentRepository.GetInfo();
        return establishment != null ? EstablishmentMapper.ToDto(establishment) : null;
    }
}
