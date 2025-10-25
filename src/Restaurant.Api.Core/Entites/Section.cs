using System.Collections.Generic;
namespace Restaurant.Api.Core.Entities;

public class Section {
    public required string Id { get; set; }
    public required string Name { get; set; }
    public required List<Product> Products { get; set; }
}