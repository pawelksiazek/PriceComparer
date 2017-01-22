using System.Collections.Generic;
using System.Linq;
using Common.DTO.BusinessModels;
using PriceComparer.BusinessLayer.Interfaces;

namespace PriceComparer.BusinessLayer.Comparers
{
    public class ItemsComparer<T> : IItemsComparer<T> where T : Item<T>
    {
        public List<T> GetCheapestItems(List<T> items)
        {
            List<T> cheapestItems = null;

            foreach (var item in items)
            {
                if (item.Price != null)
                {
                    if (cheapestItems == null)
                    {
                        cheapestItems = new List<T> { item };
                        continue;
                    }

                    if (item.Price < cheapestItems.First().Price)
                    {
                        cheapestItems = new List<T> { item };
                    }
                    else if (item.Price == cheapestItems.First().Price)
                    {
                        cheapestItems.Add(item);
                    }
                }
            }
            return cheapestItems;
        }
    }
}
