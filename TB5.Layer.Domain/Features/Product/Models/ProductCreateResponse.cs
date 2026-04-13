using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TB5.Layer.Domain.Features.Product.Models;

public class ProductCreateResponse
{
    public bool IsSuccess { get; set; }
    public string Message { get; set; }
    public int Id { get; set; }
}
