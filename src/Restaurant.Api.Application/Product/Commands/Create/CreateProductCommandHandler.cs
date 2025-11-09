using MediatR;
using Restaurant.Api.Application.Category.Mapper;
using Restaurant.Api.Core.Interfaces;

namespace Restaurant.Api.Application.Product.Commands.CreateProduct;

public class CreateProductCommandHandler( IProductRepository productRepository) : IRequestHandler<CreateProductCommand, Guid>
{
    private readonly IProductRepository _productRepository = productRepository;
    public async Task<Guid> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var product = ProductMapper.Create(request);
            await _productRepository.AddProduct(Guid.Parse(request.CategoryId), product);
            return product.Id;
        }
        catch (System.Exception)
        {
            throw;
        }
    
    }
}