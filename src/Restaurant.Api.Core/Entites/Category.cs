using System.Collections.Generic;
namespace Restaurant.Api.Core.Entities;

public class Category {
    public required Guid Id { get; set; }
    public required string Name { get; set; }
    public required List<Product> Products { get; set; }
}