# TB5.ConsoleApp.EFCoreSample

```bash
dotnet tool install --global dotnet-ef --version 9.0.11
```

```bash
dotnet ef dbcontext scaffold "Server=.;Database=Batch5MiniPOS;User ID=sa;Password=sasa@123;TrustServerCertificate=True;" Microsoft.EntityFrameworkCore.SqlServer -o AppDbContextModels -c AppDbContext -f
```

