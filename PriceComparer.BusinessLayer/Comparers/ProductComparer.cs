using System.Collections.Generic;
using System.Linq;
using PriceComparer.BusinessLayer.Interfaces;
using PriceComparer.BusinessLayer.Models;

namespace PriceComparer.BusinessLayer.Comparers
{
    public class ProductComparer : IProductComparer
    {
        public Product GetCheapestProduct(List<Product> products)
        {
            var cheapestProduct = products.First();

            foreach (var product in products)
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
