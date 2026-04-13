# TB5.ConsoleApp.EFCoreSample

```bash
dotnet tool install --global dotnet-ef --version 9.0.11
```

```bash
dotnet ef dbcontext scaffold "Server=.;Database=Batch5MiniPOS;User ID=sa;Password=sasa@123;TrustServerCertificate=True;" Microsoft.EntityFrameworkCore.SqlServer -o AppDbContextModels -c AppDbContext -f
```

```sql
USE [Batch5MiniPOS]
GO

UPDATE [dbo].[Tbl_Product]
   SET 
      [CreatedDateTime] = GetDate()
      ,[IsDelete] = 0
 
GO
```

```sql
USE [Batch5MiniPOS]
GO

UPDATE [dbo].[Tbl_Sale]
   SET 
      [CreatedDateTime] = GETDATE()
      ,[ModifiedDateTime] = GETDATE()
      ,[IsDelete] = 0
GO
```


```sql
USE [Batch5MiniPOS]
GO

/****** Object:  Table [dbo].[Tbl_Product]    Script Date: 2026-03-22 4:07:41 pm ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Tbl_ProductCategory](
	[ProductCategoryId] [int] IDENTITY(1,1) NOT NULL,
	[ProductCategoryName] [varchar](100) NOT NULL,
	[CreatedDateTime] [datetime] NOT NULL,
	[ModifiedDateTime] [datetime] NULL,
	[IsDelete] [bit] NOT NULL,
 CONSTRAINT [PK_Tbl_ProductCategory] PRIMARY KEY CLUSTERED 
(
	[ProductCategoryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

```
