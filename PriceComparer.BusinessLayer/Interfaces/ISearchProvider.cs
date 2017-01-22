using System.Collections.Generic;
using Common.DTO.BusinessModels;

namespace PriceComparer.BusinessLayer.Interfaces
{
    public interface ISearchProvider
    {
        List<Book> SearchItemsByName(string itemName);
        List<Book> GetItemsById(string itemId);
    }
}
