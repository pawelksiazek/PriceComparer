using System.Collections.Generic;

namespace Infrastructure.Common.Interfaces
{
    public interface IItemsRepository<T>
    {
        List<T> SearchItemsByName(string itemName);
        T GetItemById(int itemId);
    }
}
