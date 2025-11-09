using FluentValidation;

namespace Restaurant.Api.Application.User.Queries.GetUserById;

public class GetUserByIdQueryValidator : AbstractValidator<GetUserByIdQuery>
{
    public GetUserByIdQueryValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Id is required")
            .Must(id => Guid.TryParse(id, out _)).WithMessage("Type invalid");

    }
}