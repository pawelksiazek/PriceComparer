using System.Collections.Generic;
using System.Linq;
using Common.DTO.BusinessModels;
using PriceComparer.BusinessLayer.Interfaces;

namespace PriceComparer.BusinessLayer.Comparers
{
    public class ItemsComparer<T> : IItemsComparer<T> where T : Item<T>
    {
        public T GetCheapestItem(List<T> items)
        {
            var cheapestItem = items.First();

            foreach (var product in items)
            {
                if (product.Price < cheapestItem.Price)
                {
                    cheapestItem = product;
                }
            }
            return cheapestItem;
        }
    }
}
