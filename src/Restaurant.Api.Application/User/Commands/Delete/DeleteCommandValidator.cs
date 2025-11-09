using FluentValidation;

namespace Restaurant.Api.Application.User.Commands.Delete;

public class DeleteCommandValidator : AbstractValidator<DeleteCommand>
{
    public DeleteCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Id is required")
            .Must(id => Guid.TryParse(id, out _)).WithMessage("Type invalid");

    }
}