using System.Collections.Generic;
using PriceComparer.BusinessLayer.Interfaces;
using PriceComparer.BusinessLayer.Models;
using PriceComparer.BusinessLayer.Settings;

namespace PriceComparer.BusinessLayer.Providers
{
    public class SearchProvider : ISearchProvider
    {
        private readonly IComparerSettingsProvider<Book> _comparerSettingsProvider;

        public SearchProvider()
        {
            _comparerSettingsProvider = ComparerSettingsProvider<Book>.Instance;

        }

        public List<Book> SearchItemsByName(string itemName)
        {
            var itemsFound = new List<Book>();

            foreach (var shop in _comparerSettingsProvider.ComparerSettings.AvailableShops)
            {
                itemsFound.AddRange(shop.ItemsRepository.SearchItemsByName(itemName));
            }

            return itemsFound;
        }

        public List<Book> GetItemsById(int itemId)
        {
            var itemsFound = new List<Book>();

            foreach (var shop in _comparerSettingsProvider.ComparerSettings.AvailableShops)
            {
                itemsFound.Add(shop.ItemsRepository.GetItemById(itemId));
            }

            return itemsFound;
        }
    }
}
