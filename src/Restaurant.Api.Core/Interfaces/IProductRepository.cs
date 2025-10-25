using Restaurant.Api.Core.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurant.Api.Core.Interfaces;

public interface IProductRepository {
    Task<List<Product>> GetAllProducts();
    Task<List<Section>> GetSections();
    Task<List<Product>> GetAllProductsBySectionId(Guid sectionId);
}

