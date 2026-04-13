using Dapper;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TB5.ConsoleApp.DapperSample.DapperSample;

public class ProductSerice
{
    private string connectionString = Environment.GetEnvironmentVariable("LOCAL_SQL_CONNECTION_STRING")!;

    public void Create(string productName, decimal productPrice)
    {
        string query = @"
                INSERT INTO [dbo].[Tbl_Product]
                       ([Name]
                       ,[Price]
                       ,[isDelete])
                 VALUES
                       (@Name
                       ,@Price
                       ,@isDelete)";

        using IDbConnection connection = new SqlConnection(connectionString);
        connection.Open();

        int result = connection.Execute(query, new
        {
            Name = productName,
            Price = productPrice,
            isDelete = 0,
        });

        connection.Close();

        string mesage = result > 0 ? "Product creation successful." : "Product creation failed.";
        Console.WriteLine(mesage);

    }

    public void Read()
    {
        string query = @"
                SELECT [Id]
                      ,[Name]
                      ,[Price]
                  FROM [dbo].[Tbl_Product]
                  WHERE isDelete = 0";

        using IDbConnection connection = new SqlConnection(connectionString);
        connection.Open();

        List<Product> results = connection.Query<Product>(query).ToList();

        connection.Close();

        foreach (Product row in results)
        {
            Console.WriteLine($"Id: {row.Id}, Name: {row.Name}, Price: {row.Price}");
        }
    }

    public void Edit(int productId)
    {
        string query = @"
                SELECT [Id]
                      ,[Name]
                      ,[Price]
                  FROM [dbo].[Tbl_Product]
                  WHERE Id = @Id
                  AND [isDelete] = 0";

        using IDbConnection connection = new SqlConnection(connectionString);
        connection.Open();

        Product? result = connection.QueryFirstOrDefault<Product>(query, new { Id = productId });

        connection.Close();

        if (result is null)
        {
            Console.WriteLine($"Product Id {productId} not found.");
            return;
        }

        Console.WriteLine($"Id: {result.Id}, Name: {result.Name}, Price: {result.Price}");
    }

    public void Update(int productId, string productName, decimal productPrice)
    {
        string query = @"
                UPDATE [dbo].[Tbl_Product]
                SET [Name] = @Name
                    ,[Price] = @Price
                WHERE Id = @Id";

        using IDbConnection connection = new SqlConnection(connectionString);
        connection.Open();

        int result = connection.Execute(query, new
        {
            Id = productId,
            Name = productName,
            Price = productPrice
        });

        connection.Close();

        string mesage = result > 0 ? "Product Update successful." : "Product Update failed.";
        Console.WriteLine(mesage);
    }

    public void Delete(int productId)
    {
        string query = @"
                UPDATE [dbo].[Tbl_Product]
                SET isDelete = 1
                WHERE Id = @Id";

        using IDbConnection connection = new SqlConnection(connectionString);
        connection.Open();

        int result = connection.Execute(query, new { Id = productId });

        connection.Close();

        string mesage = result > 0 ? $"Delete Product Id {productId} successful." : $"Delete Product Id {productId} failed.";
        Console.WriteLine(mesage);
    }
}

public class Product
{
    public int Id { get; set; }

    public string Name { get; set; }

    public decimal Price { get; set; }
}
