using TB5.Layer.Domain.Features.Product;
using TB5.Layer.Domain.Features.Product.Models;
using TB5.NLayer.Database.AppDbContextModels;

namespace TB5.NLayer.MinimalAPI.Features
{
    public static class ProductEndPoints
    {
        public static void MapProductEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapGet("/api/product", () =>
            {
                ProductService service = new ProductService();
                return Results.Ok(service.GetProducts());
            })
            .WithName("GetProducts")
            .WithOpenApi();

            app.MapGet("/api/product/{id}", (int id) =>
            {
                ProductService service = new ProductService();
                TblProduct? product = service.GetProduct(id);
                if (product == null)
                {
                    return Results.NotFound(new { Message = "Product not found." });
                }
                return Results.Ok(product);
            })
            .WithName("GetProductById")
            .WithOpenApi();

            app.MapPost("/api/product", (ProductCreateRequest request) =>
            {
                ProductService service = new ProductService();
                var response = service.CreateProduct(request);
                return response.IsSuccess ? Results.Ok(response) : Results.BadRequest(response);
            })
            .WithName("CreateProduct")
            .WithOpenApi();

            app.MapPut("/api/product/{id}", (int id, ProductUpdateRequest request) =>
            {
                ProductService service = new ProductService();
                bool isSuccess = service.UpdateProduct(id, request);
                return isSuccess ? Results.Ok(new { IsSuccess = true, Message = "Product updated successfully." }) : Results.BadRequest(new { IsSuccess = false, Message = "Failed to update product." });
            })
            .WithName("UpdateProduct")
            .WithOpenApi();

            app.MapDelete("/api/product/{id}", (int id) =>
            {
                ProductService service = new ProductService();
                bool isSuccess = service.DeleteProduct(id);
                return isSuccess ? Results.Ok(new { IsSuccess = true, Message = "Product deleted successfully." }) : Results.BadRequest(new { IsSuccess = false, Message = "Failed to delete product." });
            })
            .WithName("DeleteProduct")
            .WithOpenApi();
        }
    }
}
