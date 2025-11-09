using FluentValidation;

namespace Restaurant.Api.Application.Product.Commands.UpdateProduct;

public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
{
    public UpdateProductCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Id is required")
            .Must(id => Guid.TryParse(id, out _)).WithMessage("Type invalid");

        RuleFor(x => x.CategoryId)
            .NotEmpty().WithMessage("CategoryId is required")
            .Must(id => Guid.TryParse(id, out _)).WithMessage("Type invalid");
            
        When(x => x.Name != null, () =>
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required");
        });

        When(x => x.Name != null, () =>
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required")
                .Must(name => name != null && name.Length > 100).WithMessage("Name must be less than 100 characters");
        });

        When(x => x.Description != null, () =>
        {
            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Description is required")
                .Must(description => description != null && description.Length > 200).WithMessage("Description must be less than 200 characters");
        });

        When(x => x.Price != null, () =>
        {
            RuleFor(x => x.Price)
                .NotEmpty().WithMessage("Price is required");
        });

        When(x => x.ImageUrl != null, () =>
        {
            RuleFor(x => x.ImageUrl)
                .NotEmpty().WithMessage("ImageUrl is required")
                .Must(image => image != null && image.Length > 500).WithMessage("ImageUrl must be less than 500 characters");
        });

        When(x => x.Available != null, () =>
        {
            RuleFor(x => x.Available)
                .NotEmpty().WithMessage("Available is required");
        });

    }
}