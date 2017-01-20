using System.Collections.Generic;
using PriceComparer.BusinessLayer.Models;

namespace PriceComparer.BusinessLayer.Interfaces
{
    public interface IProductComparer
    {
        Product GetCheapestProduct(List<Product> products);
    }
}
