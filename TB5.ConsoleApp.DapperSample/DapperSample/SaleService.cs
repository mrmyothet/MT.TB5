using Dapper;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace TB5.ConsoleApp.DapperSample.DapperSample;

public class SaleService
{
    private string connectionString = Environment.GetEnvironmentVariable("LOCAL_SQL_CONNECTION_STRING")!;

    public void Create(int productId, decimal price, int quantity, DateTime? saleDate)
    {
        string query = @"
                INSERT INTO [dbo].[Tbl_Sale]
                       ([ProductId]
                       ,[Price]
                       ,[Quantity]
                       ,[SaleDate])
                 VALUES
                       (@ProductId
                       ,@Price
                       ,@Quantity
                       ,@SaleDate)";

        using IDbConnection connection = new SqlConnection(connectionString);
        connection.Open();

        int result = connection.Execute(query, new
        {
            ProductId = productId,
            Price = price,
            Quantity = quantity,
            SaleDate = saleDate
        });

        connection.Close();

        string message = result > 0 ? "Sale creation successful." : "Sale creation failed.";
        Console.WriteLine(message);
    }

    public void Read()
    {
        string query = @"
                SELECT [SaleId]
                      ,[ProductId]
                      ,[Price]
                      ,[Quantity]
                      ,[SaleDate]
                  FROM [dbo].[Tbl_Sale]";

        using IDbConnection connection = new SqlConnection(connectionString);
        connection.Open();

        List<Sale> results = connection.Query<Sale>(query).ToList();

        connection.Close();

        foreach (Sale row in results)
        {
            Console.WriteLine($"SaleId: {row.SaleId}, ProductId: {row.ProductId}, Price: {row.Price}, Quantity: {row.Quantity}, SaleDate: {row.SaleDate}");
        }
    }

    public void Edit(int saleId)
    {
        string query = @"
                SELECT [SaleId]
                      ,[ProductId]
                      ,[Price]
                      ,[Quantity]
                      ,[SaleDate]
                  FROM [dbo].[Tbl_Sale]
                  WHERE SaleId = @SaleId";

        using IDbConnection connection = new SqlConnection(connectionString);
        connection.Open();

        Sale? result = connection.QueryFirstOrDefault<Sale>(query, new { SaleId = saleId });

        connection.Close();

        if (result is null)
        {
            Console.WriteLine($"Sale Id {saleId} not found.");
            return;
        }

        Console.WriteLine($"SaleId: {result.SaleId}, ProductId: {result.ProductId}, Price: {result.Price}, Quantity: {result.Quantity}, SaleDate: {result.SaleDate}");
    }

    public void Update(int saleId, int productId, decimal price, int quantity, DateTime? saleDate)
    {
        string query = @"
                UPDATE [dbo].[Tbl_Sale]
                SET [ProductId] = @ProductId
                    ,[Price] = @Price
                    ,[Quantity] = @Quantity
                    ,[SaleDate] = @SaleDate
                WHERE SaleId = @SaleId";

        using IDbConnection connection = new SqlConnection(connectionString);
        connection.Open();

        int result = connection.Execute(query, new
        {
            SaleId = saleId,
            ProductId = productId,
            Price = price,
            Quantity = quantity,
            SaleDate = saleDate
        });

        connection.Close();

        string message = result > 0 ? "Sale Update successful." : "Sale Update failed.";
        Console.WriteLine(message);
    }

    public void Delete(int saleId)
    {
        string query = @"
                DELETE 
                FROM [dbo].[Tbl_Sale]
                WHERE SaleId=@SaleId";

        using IDbConnection connection = new SqlConnection(connectionString);
        connection.Open();

        int result = connection.Execute(query, new { SaleId = saleId });

        connection.Close();

        string message = result > 0 ? $"Delete Sale Id {saleId} successful." : $"Delete Sale Id {saleId} failed.";
        Console.WriteLine(message);
    }
}

public class Sale
{
    public int SaleId { get; set; }

    public int ProductId { get; set; }

    public decimal Price { get; set; }

    public int Quantity { get; set; }

    public DateTime? SaleDate { get; set; }
}
