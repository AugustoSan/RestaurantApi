using Restaurant.Api.Core.Entities;
using Restaurant.Api.Core.Interfaces;

namespace Restaurant.Api.Infraestructure.Repositories;

public class ProductRepository : IProductRepository {
    private List<Section> sections = new List<Section> {
                new Section {
                    Id = "bdf9bfef-add7-48e7-ad3b-fcf02c144d6a",
                    Name = "Bebidas calientes",
                    Products = new List<Product>{
                        new(){ 
                            Id = "bdf7bfef-add7-48e7-ad3b-fcf02c144d6a",
                            Name = "Café capuchino", 
                            Price = 40.00,
                            Description = "Café capuchino",
                            Category = "Bebidas calientes",
                            ImageUrl = "./assets/images/cafe-capuchino.png"
                        },
                        new(){
                            Id = "42c5f241-4511-4af5-bda8-294426628e85",
                            Name = "Café americano", 
                            Price = 40.00,
                            Description = "Café americano",
                            Category = "Bebidas calientes",
                            ImageUrl = "./assets/images/cafe-americano.png"
                        },
                        new(){
                            Id = "d122c22d-525d-4c2c-882e-5152467b2b3b",
                            Name = "Café latte", 
                            Price = 40.00,
                            Description = "Café latte",
                            Category = "Bebidas calientes",
                            ImageUrl = "./assets/images/cafe-latte.png"
                        },
                        new(){
                            Id = "f7382ec0-0c3e-4063-bc84-3de6ba868752",
                            Name = "Café macchiato", 
                            Price = 40.00,
                            Description = "Café macchiato",
                            Category = "Bebidas calientes",
                            ImageUrl = "./assets/images/cafe-macchiato.png"
                        }
                    }
                },
                new Section() {
                    Id = "bdf9bfef-add7-48e7-ad3b-fcf02c144d5a",
                    Name = "Bebidas frías",
                    Products = new List<Product>{
                        new(){
                            Id = "d6ca09f1-83f2-4551-ae13-8883a459354b",
                            Name = "Frappuccino", 
                            Price = 40.00,
                            Description = "Frappuccino",
                            Category = "Bebidas frías",
                            ImageUrl = "./assets/images/frappuccino.png"
                        },
                        new(){
                            Id = "81e3a87a-ef58-4e09-8875-af1354e99ffe",
                            Name = "Frapé de taro", 
                            Price = 40.00,
                            Description = "Frapé de taro",
                            Category = "Bebidas frías",
                            ImageUrl = "./assets/images/frappe-taro.png"
                        },
                        new(){
                            Id = "e626165d-2b4c-4b4c-8b4c-2b4c2b4c2b4c",
                            Name = "Frapé de yogurt", 
                            Price = 40.00,
                            Description = "Frapé de yogurt",
                            Category = "Bebidas frías",
                            ImageUrl = "./assets/images/frappe-yogurt.png"
                        },
                        new(){
                            Id = "d6ca09f1-83f2-4551-ae13-8883a459354b",
                            Name = "Frapé cookies & cream", 
                            Price = 40.00,
                            Description = "Frapé cookies & cream",
                            Category = "Bebidas frías",
                            ImageUrl = "./assets/images/frappe-cookies-and-cream.png"
                        },
                        new(){
                            Id = "683f2f40-7e25-4115-ad83-f5e0e4400fbe",
                            Name = "Frapé de mazapán", 
                            Price = 40.00,
                            Description = "Frapé de mazapán",
                            Category = "Bebidas frías",
                            ImageUrl = "./assets/images/frappe-mazapan.png"
                        }
                    }
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
