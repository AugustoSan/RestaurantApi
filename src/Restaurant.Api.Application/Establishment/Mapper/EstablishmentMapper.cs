using Restaurant.Api.Application.Establishment.Dtos;
using CoreEstablishment = Restaurant.Api.Core.Entities.Establishment;

namespace Restaurant.Api.Application.Establishment.Mapper;

public class EstablishmentMapper {
    public static EstablishmentDto ToDto(CoreEstablishment Establishment) {
        if (Establishment == null)
            throw new ArgumentNullException(nameof(Establishment));
        return new EstablishmentDto {
            Id = Establishment.Id.ToString(),
            Name = Establishment.Name,
            Description = Establishment.Description,
            Token = Establishment.Token,
            Address = Establishment.Address,
            Phone = Establishment.Phone,
            Logo = Establishment.Logo,
        };
    }
    public static CoreEstablishment ToEntity(EstablishmentDto EstablishmentDto) {
        if (EstablishmentDto == null)
            throw new ArgumentNullException(nameof(EstablishmentDto));
        return new CoreEstablishment {
            Id = Guid.Parse(EstablishmentDto.Id),
            Name = EstablishmentDto.Name,
            Description = EstablishmentDto.Description,
            Token = EstablishmentDto.Token,
            Address = EstablishmentDto.Address,
            Phone = EstablishmentDto.Phone,
            Logo = EstablishmentDto.Logo,
        };
    }
}