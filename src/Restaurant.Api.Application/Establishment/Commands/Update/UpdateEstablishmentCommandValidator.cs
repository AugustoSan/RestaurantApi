using FluentValidation;

namespace Restaurant.Api.Application.Establishment.Commands;

public class UpdateEstablishmentCommandValidator : AbstractValidator<UpdateEstablishmentCommand>
{
    public UpdateEstablishmentCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Name is required")
            .Must(id => Guid.TryParse(id, out _)).WithMessage("Type invalid");

        // TODO Faltaria validar el token
        RuleFor(x => x.Token)
            .NotEmpty().WithMessage("Token is required");

        When(x => x.Name != null, () =>
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required");
        });

        When(x => x.Description != null, () =>
        {
            RuleFor(x => x.Description).NotEmpty().WithMessage("Description is required");
        });

        When(x => x.Address != null, () =>
        {
            RuleFor(x => x.Address).NotEmpty().WithMessage("Address is required");
        });

        When(x => x.Phone != null, () =>
        {
            RuleFor(x => x.Phone).NotEmpty().WithMessage("Phone is required");
        });

        When(x => x.Logo != null, () =>
        {
            RuleFor(x => x.Logo).NotEmpty().WithMessage("Logo is required");
        });

    }
}