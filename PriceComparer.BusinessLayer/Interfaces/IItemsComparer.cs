using System.Collections.Generic;

namespace PriceComparer.BusinessLayer.Interfaces
{
    public interface IItemsComparer<T>
    {
        T GetCheapestItem(List<T> items);
    }
}
