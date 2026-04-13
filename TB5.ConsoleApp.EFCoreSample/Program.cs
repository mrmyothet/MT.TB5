using TB5.ConsoleApp.EFCoreSample;

var productService = new ProductService();

productService.Create("Sample Product", 9.99m);
Console.WriteLine();

productService.Read();
Console.WriteLine();

productService.Update(1006, "Macbook Neo", 599);
Console.WriteLine();

productService.Delete(1007);
Console.WriteLine();

productService.Edit(1008);
Console.WriteLine();

Console.ReadLine();
