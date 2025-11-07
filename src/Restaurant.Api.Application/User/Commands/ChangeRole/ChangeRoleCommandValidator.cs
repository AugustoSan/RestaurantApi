using FluentValidation;

namespace Restaurant.Api.Application.User.Commands.ChangeRole;

public class ChangeRoleCommandValidator : AbstractValidator<ChangeRoleCommand>
{
    public ChangeRoleCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Id is required")
            .Must(id => Guid.TryParse(id, out _)).WithMessage("Type invalid");
        RuleFor(x => x.RoleId)
            .NotEmpty().WithMessage("RoleId is required")
            .Must(role => Guid.TryParse(role, out _)).WithMessage("Type invalid");
    }
}