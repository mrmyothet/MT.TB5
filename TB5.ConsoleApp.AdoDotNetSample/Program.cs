using Microsoft.Data.SqlClient;
using TB5.ConsoleApp.AdoDotNetSample;


var service = new AdoDotNetService();
var result = await service.CanConnectSQLServerAsync();
Console.WriteLine(result ? "SQL Server connection successful." : "SQL Server connection failed.");

//var example = new Student();
//example.RunStudentClassExample();

//Console.WriteLine("Creating a new product...");
//Console.WriteLine("Enter product name:");
//string productName = Console.ReadLine();

//Console.WriteLine("Enter product price:");
//int productPrice = Convert.ToInt32(Console.ReadLine());

//service.Create(productName, productPrice);

//Console.WriteLine("Updating an exising product...");
//Console.WriteLine("Enter product Id you want to update:");
//int productId = Convert.ToInt32(Console.ReadLine());

//Console.WriteLine("Enter updated product name :");
//productName = Console.ReadLine();

//Console.WriteLine("Enter updated product price:");
//productPrice = Convert.ToInt32(Console.ReadLine());

//service.Update(productId, productName, productPrice);

//service.Delete(1002);

//service.Read();

service.Edit(4);

Console.ReadLine();

