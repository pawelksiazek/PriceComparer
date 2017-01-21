using System.Collections.Generic;
using PriceComparer.BusinessLayer.Models;

namespace PriceComparer.BusinessLayer.Interfaces
{
    public interface IItemsComparer
    {
        Product GetCheapestItem(List<Product> items);
    }
}
