using FluentValidation;

namespace Restaurant.Api.Application.Product.Queries.GetProductById;

public class GetProductByIdQueryValidator : AbstractValidator<GetProductByIdQuery>
{
    public GetProductByIdQueryValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Id is required")
            .Must(id => Guid.TryParse(id, out _)).WithMessage("Type invalid");
        RuleFor(x => x.CategoryId)
            .NotEmpty().WithMessage("CategoryId is required")
            .Must(id => Guid.TryParse(id, out _)).WithMessage("Type invalid");
    }
}