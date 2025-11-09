using FluentValidation;

namespace Restaurant.Api.Application.User.Commands.Update;

public class UpdateCommandValidator : AbstractValidator<UpdateCommand>
{
    public UpdateCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Id is required")
            .Must(id => Guid.TryParse(id, out _)).WithMessage("Type invalid");
        When(x => x.Name != null, () =>
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required");
        });

        When(x => x.Username != null, () =>
        {
            RuleFor(x => x.Username).NotEmpty().WithMessage("Username is required");
        });

    }
}