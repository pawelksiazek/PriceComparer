﻿using System.Collections.Generic;
using Common.DTO.BusinessModels;
using PriceComparer.BusinessLayer.Interfaces;
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
                if (shop.IsEnabled) itemsFound.AddRange(shop.ItemsRepository.SearchItemsByName(itemName));
            }

            return itemsFound;
        }

        public List<Book> GetItemsById(string itemId)
        {
            var itemsFound = new List<Book>();

            foreach (var shop in _comparerSettingsProvider.ComparerSettings.AvailableShops)
            {
                if (shop.IsEnabled) itemsFound.Add(shop.ItemsRepository.GetItemById(itemId));
            }

            return itemsFound;
        }
    }
}
