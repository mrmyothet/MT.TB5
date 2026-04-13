using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TB5.Layer.Domain.Features.Product.Models;

public class ProductUpdateRequest
{
    public string Name { get; set; } = null!;
    public decimal Price { get; set; }
}
