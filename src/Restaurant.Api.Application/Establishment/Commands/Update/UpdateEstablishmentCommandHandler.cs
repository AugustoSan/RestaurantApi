using MediatR;
using Restaurant.Api.Application.Establishment.Mapper;
using Restaurant.Api.Core.Interfaces;

namespace Restaurant.Api.Application.Establishment.Commands;

public class UpdateEstablishmentCommandHandler(IEstablishmentRepository establishmentRepository) : IRequestHandler<UpdateEstablishmentCommand, Guid>
{
    private readonly IEstablishmentRepository _establishmentRepository = establishmentRepository;
    public async Task<Guid> Handle(UpdateEstablishmentCommand request, CancellationToken cancellationToken)
    {
        var establishment = await _establishmentRepository.GetInfo();
        if (establishment == null)
            throw new ArgumentNullException(nameof(establishment));
        var entity = EstablishmentMapper.UpdateEntity(request, establishment);
        await _establishmentRepository.Update(entity);
        return Guid.Empty;
    
    }
}