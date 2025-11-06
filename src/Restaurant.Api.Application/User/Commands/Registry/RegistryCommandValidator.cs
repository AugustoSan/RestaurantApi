using FluentValidation;

namespace Restaurant.Api.Application.User.Commands.Registry;

public class RegistryCommandValidator : AbstractValidator<RegistryCommand>
{
    public RegistryCommandValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required");
        RuleFor(x => x.Username).NotEmpty().WithMessage("Username is required");
        RuleFor(x => x.Password).NotEmpty().WithMessage("Password is required");
        RuleFor(x => x.RoleId)
            .NotEmpty().WithMessage("RoleId is required")
            .Must(roleId => Guid.TryParse(roleId, out _)).WithMessage("Type invalid");

    }
}