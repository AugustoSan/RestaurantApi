using FluentValidation;

namespace Restaurant.Api.Application.Product.Queries.GetAllProductsByCategory;

public class GetAllProductsByCategoryQueryValidator : AbstractValidator<GetAllProductsByCategoryQuery>
{
    public GetAllProductsByCategoryQueryValidator()
    {
        RuleFor(x => x.CategoryId)
            .NotEmpty().WithMessage("CategoryId is required")
            .Must(id => Guid.TryParse(id, out _)).WithMessage("Type invalid");
    }
}