using TB5.Layer.Domain.Features.Product;
using TB5.Layer.Domain.Features.Product.Models;

Console.WriteLine("Hello, World!");

ProductService productService = new ProductService();
Console.WriteLine("\n Get Products");
var products = productService.GetProducts();
Console.WriteLine($"Total Products: {products.Count}");

// Create Product
var createRequest = new ProductCreateRequest { Name = "Console Test Product", Price = 99.99m };
var createResponse = productService.CreateProduct(createRequest);
Console.WriteLine($"Create Product Success: {createResponse.IsSuccess}, Id: {createResponse.Id}");

Console.ReadLine();