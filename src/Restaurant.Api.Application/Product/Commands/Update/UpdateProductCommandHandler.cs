using MediatR;
using Restaurant.Api.Application.Category.Mapper;
using Restaurant.Api.Core.Interfaces;

namespace Restaurant.Api.Application.Product.Commands.UpdateProduct;

public class UpdateCommandHandler( IProductRepository productRepository) : IRequestHandler<UpdateProductCommand, Guid>
{
    private readonly IProductRepository _productRepository = productRepository;
    public async Task<Guid> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var product = await _productRepository.GetProductById(Guid.Parse(request.CategoryId), Guid.Parse(request.Id));
            if (product == null) return Guid.Empty;
            var productMapper = ProductMapper.ToUpdate(request, product);
            await _productRepository.UpdateProduct(Guid.Parse(request.CategoryId), Guid.Parse(request.Id), productMapper);
            return Guid.Parse(request.Id);
        }
        catch (System.Exception)
        {
            throw;
        }
    
    }
}