using MongoDB.Driver;
using Restaurant.Api.Core.Entities;
using Restaurant.Api.Core.Interfaces;

namespace Restaurant.Api.Infrastructure.Persistance.Seeders;

public class SeederPersistance(
    ICategoryRepository categoryRepository,
    IRoleRepository roleRepository,
    IUserRepository userRepository
) : ISeederPersistance
{
    private readonly ICategoryRepository _categoryRepository = categoryRepository;
    private readonly IRoleRepository _roleRepository = roleRepository;
    private readonly IUserRepository _userRepository = userRepository;
    private readonly Guid _adminRoleId = Guid.NewGuid();
    private readonly Guid _userRoleId = Guid.NewGuid();
    
    public async Task SeedData()
    {
        await SeedRoles();
        await SeedUsers();
        await SeedCategories();
    }

    private async Task SeedRoles()
    {
        var roles = await _roleRepository.GetAllRoles();
        if (roles.Count == 0)
        {
            await _roleRepository.AddRole(new Role
            {
                Id =_adminRoleId,
                Name = "Administrador",
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            });
            await _roleRepository.AddRole(new Role
            {
                Id = _userRoleId,
                Name = "Usuario",
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            });
        }
    }

    private async Task SeedUsers()
    {
        var roles = await _roleRepository.GetAllRoles();
        var users = await _userRepository.GetAllUsers();
        if (users.Count == 0 && roles.Count > 0)
        {
            await _userRepository.AddUser(new User
            {
                Id = Guid.NewGuid(),
                Name = "Administrador",
                Username = "admin",
                Password = "admin",
                RoleId = _adminRoleId,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            });
        }
    }

    private async Task SeedCategories()
    {
        var categories = await _categoryRepository.GetAllCategories();
        if (categories.Count == 0)
        {
            await _categoryRepository.AddCategory(new Category
            {
                Id = Guid.NewGuid(),
                Name = "Bebidas calientes",
                Products = new List<Product>{
                    new() {
                        Id = Guid.NewGuid(),
                        Name = "Café capuchino", 
                        Price = 40.00,
                        Description = "Café capuchino",
                        Available = true
                    },
                    new() {
                        Id = Guid.NewGuid(),
                        Name = "Café americano", 
                        Price = 40.00,
                        Description = "Café americano",
                        Available = true
                    },
                    new(){
                        Id = Guid.NewGuid(),
                        Name = "Café latte", 
                        Price = 40.00,
                        Description = "Café latte",
                        Available = true
                    },
                    new(){
                        Id = Guid.NewGuid(),
                        Name = "Café macchiato", 
                        Price = 40.00,
                        Description = "Café macchiato",
                        Available = true
                    }
                }
            });
            await _categoryRepository.AddCategory(new Category
            {
                Id = Guid.NewGuid(),
                Name = "Bebidas frías",
                Products = new List<Product>{
                    new(){
                        Id = Guid.NewGuid(),
                        Name = "Frappuccino", 
                        Price = 40.00,
                        Description = "Frappuccino",
                        Available = true
                    },
                    new(){
                        Id = Guid.NewGuid(),
                        Name = "Frapé de taro", 
                        Price = 40.00,
                        Description = "Frapé de taro",
                        Available = true
                    },
                    new(){
                        Id = Guid.NewGuid(),
                        Name = "Frapé de yogurt", 
                        Price = 40.00,
                        Description = "Frapé de yogurt",
                        Available = true
                    },
                    new(){
                        Id = Guid.NewGuid(),
                        Name = "Frapé cookies & cream", 
                        Price = 40.00,
                        Description = "Frapé cookies & cream",
                        Available = true
                    },
                    new(){
                        Id = Guid.NewGuid(),
                        Name = "Frapé de mazapán", 
                        Price = 40.00,
                        Description = "Frapé de mazapán",
                        Available = true
                    }
                }
            });
        }
    }
}
