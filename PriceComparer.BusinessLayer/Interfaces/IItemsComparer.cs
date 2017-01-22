using System.Collections.Generic;

namespace PriceComparer.BusinessLayer.Interfaces
{
    public interface IItemsComparer<T>
    {
        List<T> GetCheapestItems(List<T> items);
    }
}
