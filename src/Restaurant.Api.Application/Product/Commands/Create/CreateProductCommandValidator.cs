using FluentValidation;

namespace Restaurant.Api.Application.Product.Commands.CreateProduct;

public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidator()
    {
        RuleFor(x => x.CategoryId)
            .NotEmpty().WithMessage("CategoryId is required")
            .Must(id => Guid.TryParse(id, out _)).WithMessage("Type invalid");
        RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required");
        RuleFor(x => x.Price).NotEmpty().WithMessage("Username is required");

        When(x => x.Description != null, () =>
        {
            RuleFor(x => x.Description).NotEmpty().WithMessage("Description is required");
        });

        When(x => x.ImageUrl != null, () =>
        {
            RuleFor(x => x.ImageUrl).NotEmpty().WithMessage("ImageUrl is required");
        });

    }
}