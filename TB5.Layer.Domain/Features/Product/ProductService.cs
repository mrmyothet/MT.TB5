using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TB5.Layer.Domain.Features.Product.Models;
using TB5.NLayer.Database.AppDbContextModels;

namespace TB5.Layer.Domain.Features.Product;

public  class ProductService
{
    public List<TblProduct> GetProducts()
    {
        AppDbContext db = new AppDbContext();
        List<TblProduct> lst = db.TblProducts.Where(x => !x.IsDelete).ToList();
        return lst;
    }

    public TblProduct? GetProduct(int id)
    {
        AppDbContext db = new AppDbContext();
        return db.TblProducts.FirstOrDefault(x => x.Id == id && !x.IsDelete);
    }

    public ProductCreateResponse CreateProduct(ProductCreateRequest request)
    {
        AppDbContext db = new AppDbContext();
        TblProduct product = new TblProduct
        {
            Name = request.Name,
            Price = request.Price,
            CreatedDateTime = DateTime.Now,
            IsDelete = false
        };
        db.TblProducts.Add(product);
        int result = db.SaveChanges();
        bool isSuccess = result > 0;
        return new ProductCreateResponse
        {
            IsSuccess = isSuccess,
            Message = isSuccess ? "Product created successfully." : "Failed to create product.",
            Id = product.Id
        };
    }

    public bool UpdateProduct(int id, ProductUpdateRequest request)
    {
        AppDbContext db = new AppDbContext();
        TblProduct? product = db.TblProducts.FirstOrDefault(x => x.Id == id && !x.IsDelete);
        if (product == null) return false;

        product.Name = request.Name;
        product.Price = request.Price;
        product.ModifiedDateTime = DateTime.Now;

        db.TblProducts.Update(product);
        int result = db.SaveChanges();
        return result > 0;
    }

    public bool DeleteProduct(int id)
    {
        AppDbContext db = new AppDbContext();
        TblProduct? product = db.TblProducts.FirstOrDefault(x => x.Id == id && !x.IsDelete);
        if (product == null) return false;

        product.IsDelete = true;
        product.ModifiedDateTime = DateTime.Now;

        db.TblProducts.Update(product);
        int result = db.SaveChanges();
        return result > 0;
    }
}
