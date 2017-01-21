using System.Collections.Generic;
using System.Linq;
using PriceComparer.BusinessLayer.Interfaces;
using PriceComparer.BusinessLayer.Models;

namespace PriceComparer.BusinessLayer.Comparers
{
    public class ItemsComparer : IItemsComparer
    {
        public Product GetCheapestItem(List<Product> items)
        {
            var cheapestProduct = items.First();

            foreach (var product in items)
            {
                if (product.Price < cheapestProduct.Price)
                {
                    cheapestProduct = product;
                }
            }
            return cheapestProduct;
        }
    }
}
