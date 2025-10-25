using Restaurant.Api.Core.Entities;
using Restaurant.Api.Core.Interfaces;

namespace Restaurant.Api.Infraestructure.Repositories;

public class ProductRepository : IProductRepository {
    private List<Section> sections = new List<Section> {
                new Section {
                    Id = "bdf9bfef-add7-48e7-ad3b-fcf02c144d6a",
                    Name = "Bebidas",
                    Products = []
                },
                new Section() {
                    Id = "bdf9bfef-add7-48e7-ad3b-fcf02c144d5a",
                    Name = "Bebidas frías",
                    Products = []
                }
            };
    

    public async Task<List<Product>> GetAllProducts() {
        // Usamos SelectMany para aplanar la lista de listas de productos en una sola lista.
        
        await Task.Delay(100);
        return sections.SelectMany(section => section.Products).ToList();
    }
    public async Task<List<Section>> GetSections() {
        await Task.Delay(100);
        return sections;
    }

    public async Task<List<Product>> GetAllProductsBySectionId(Guid sectionId) {
        // Buscamos la sección que coincida con el ID (comparando como strings).
        var section = sections.FirstOrDefault(s => s.Id == sectionId.ToString());
        
        await Task.Delay(100);
        // Si encontramos la sección, devolvemos sus Products; si no, una lista vacía.
        return section?.Products ?? new List<Product>();
    }
}
