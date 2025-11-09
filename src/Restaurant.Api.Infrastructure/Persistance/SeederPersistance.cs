using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Restaurant.Api.Core.Entities;
using Restaurant.Api.Core.Interfaces;
using Restaurant.Api.Core.Options;

namespace Restaurant.Api.Infrastructure.Persistance.Seeders;

public class SeederPersistance(
    ICategoryRepository categoryRepository,
    IRoleRepository roleRepository,
    IUserRepository userRepository,
    IEstablishmentRepository restaurantRepository,
    IOptions<InitialValue> initialValue
) : ISeederPersistance
{
    private readonly ICategoryRepository _categoryRepository = categoryRepository;
    private readonly IRoleRepository _roleRepository = roleRepository;
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IEstablishmentRepository _restaurantRepository = restaurantRepository;
    private readonly InitialValue _initialValue = initialValue.Value;
    private readonly Guid _adminRoleId = Guid.NewGuid();
    private readonly Guid _userRoleId = Guid.NewGuid();

    public async Task SeedData()
    {
        await SeedRoles();
        await SeedUsers();
        await SeedCategories();
        await SeedRestaurants();
    }

    private async Task SeedRestaurants()
    {
        if (_initialValue.Establishment == null)
            throw new ArgumentNullException("No hay algun dato initial para establecimiento");
        var establishment = _initialValue.Establishment;
        var restaurant = await _restaurantRepository.GetInfo();
        if(restaurant == null)
        {
            await _restaurantRepository.Update(new Establishment
            {
                Id = Guid.NewGuid(),
                Name = establishment.Name,
                Address = establishment.Address,
                Phone = establishment.Phone,
                Description = establishment.Description,
                Logo = establishment.Logo,
                Email = establishment.Email,
                Token = establishment.Token
            });
        }
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
        if (_initialValue.Administrator == null)
            throw new ArgumentNullException("No hay algun dato initial para el usuario administrador");
        var administrator = _initialValue.Administrator;
        var roles = await _roleRepository.GetAllRoles();
        var user = await _userRepository.GetUserByUsername(administrator.Username);
        if (roles.Count > 0)
        {
            if(user == null)
            {
                await _userRepository.AddUser(new User
                {
                    Id = Guid.NewGuid(),
                    Name = administrator.Name,
                    Username = administrator.Username,
                    Password = administrator.Password,
                    RoleId = _adminRoleId,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                });
            }
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
