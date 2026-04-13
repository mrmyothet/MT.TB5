using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TB5.ConsoleApp.Database.AppDbContextModels;

namespace TB5.ConsoleApp.EFCoreSample;

public class ProductService
{
    private readonly AppDbContext db = new AppDbContext();

    public void Create(string productName, decimal productPrice)
    {
        var product = new TblProduct
        {
            Name = productName,
            Price = productPrice,
            CreatedDateTime = DateTime.Now
        };

        int result = 0;
        try
        {
            db.TblProducts.Add(product);
            result = db.SaveChanges();

        }
        catch (Exception ex)
        {

            Console.WriteLine(ex.Message);
            Console.WriteLine(ex.InnerException.ToString());
        }

        string message = result > 0 ? "A new product created successfully." : "Failed to create a new product.";
        Console.WriteLine(message);
    }

    public void Read()
    {
        var lstProducts = db.TblProducts.ToList();

        if (lstProducts.Count == 0)
        {
            Console.WriteLine("No products found.");
            return;
        }

        foreach (TblProduct product in lstProducts)
        {
            Console.WriteLine($"Id: {product.Id}, Name: {product.Name}, Price: {product.Price}");
        }
    }

    public void Update(int productId, string productName, decimal productPrice)
    {
        var product = db.TblProducts.FirstOrDefault(p => p.Id == productId);

        if (product is null)
        {
            Console.WriteLine("Product not found.");
            return;
        }

        product.Name = productName;
        product.Price = productPrice;
        product.ModifiedDateTime = DateTime.Now;


        int result = 0;
        try
        {
            result = db.SaveChanges();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            Console.WriteLine(ex.InnerException?.ToString());
        }

        string message = result > 0 ? $"Product {product.Id} is updated successfully." : $"Failed to update the product {product.Id}.";
        Console.WriteLine(message);
    }

    public void Delete(int productId)
    {
        var product = db.TblProducts.FirstOrDefault(p => p.Id == productId);

        if (product is null)
        {
            Console.WriteLine("Product not found.");
            return;
        }

        int result = 0;
        try
        {
            db.TblProducts.Remove(product);
            result = db.SaveChanges();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            Console.WriteLine(ex.InnerException?.ToString());
        }

        string message = result > 0 ? $"Product {productId} is deleted successfully." : $"Failed to delete the product {productId}.";
        Console.WriteLine(message);
    }

    public void Edit(int productId)
    {
        var product = db.TblProducts.FirstOrDefault(p => p.Id == productId);

        if (product is null)
        {
            Console.WriteLine("Product not found.");
            return;
        }

        Console.WriteLine($"Id: {product.Id}");
        Console.WriteLine($"Name: {product.Name}");
        Console.WriteLine($"Price: {product.Price}");
    }


}
