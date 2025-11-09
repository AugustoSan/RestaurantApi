using Restaurant.Api.Application.Establishment.Commands;
using Restaurant.Api.Application.Establishment.Dtos;
using CoreEstablishment = Restaurant.Api.Core.Entities.Establishment;

namespace Restaurant.Api.Application.Establishment.Mapper;

public class EstablishmentMapper {
    public static EstablishmentDto ToDto(CoreEstablishment establishment) {
        if (establishment == null)
            throw new ArgumentNullException(nameof(establishment));
        return new EstablishmentDto {
            Id = establishment.Id.ToString(),
            Name = establishment.Name,
            Description = establishment.Description,
            Token = establishment.Token,
            Address = establishment.Address,
            Phone = establishment.Phone,
            Logo = establishment.Logo,
            Email = establishment.Email,
        };
    }
    public static CoreEstablishment ToEntity(EstablishmentDto establishmentDto)
    {
        if (establishmentDto == null)
            throw new ArgumentNullException(nameof(establishmentDto));
        return new CoreEstablishment
        {
            Id = Guid.Parse(establishmentDto.Id),
            Name = establishmentDto.Name,
            Description = establishmentDto.Description,
            Token = establishmentDto.Token,
            Address = establishmentDto.Address,
            Phone = establishmentDto.Phone,
            Logo = establishmentDto.Logo,
            Email = establishmentDto.Email,
        };
    }
    
    public static CoreEstablishment UpdateEntity(UpdateEstablishmentCommand establishmentDto, CoreEstablishment establishment) {
        if (establishmentDto == null)
            throw new ArgumentNullException(nameof(establishmentDto));
        return new CoreEstablishment {
            Id = Guid.Parse(establishmentDto.Id),
            Token = establishment.Token,
            Name = establishmentDto.Name ?? establishment.Name,
            Description = establishmentDto.Description ?? establishment.Description,
            Address = establishmentDto.Address ?? establishment.Address,
            Phone = establishmentDto.Phone ?? establishment.Phone,
            Logo = establishmentDto.Logo ?? establishment.Logo,
            Email = establishmentDto.Email ?? establishment.Email,
        };
    }
}