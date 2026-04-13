using TB5.ConsoleApp.DapperSample.DapperSample;

var service = new ProductSerice();

//service.Create("Product 1", 100);
//Console.WriteLine();

//Console.WriteLine("Displaying Products...");
//service.Read();

int productId = 1008;
//service.Edit(productId);
//Console.WriteLine();

//string productName = "Mac Mini";
//decimal productPrice = 599.55m;

//service.Update(productId, productName, productPrice);
//Console.WriteLine();

//Console.WriteLine("Displaying Products...");
//service.Read();

service.Delete(productId);
Console.WriteLine();

//Console.WriteLine("Displaying Products...");
//service.Read();

Console.ReadLine();
