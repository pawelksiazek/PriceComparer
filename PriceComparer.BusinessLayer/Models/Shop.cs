﻿using Common.DTO.BusinessModels;
using Infrastructure.Common.Interfaces;

namespace PriceComparer.BusinessLayer.Models
{
    public class Shop<T> where T : Item<T>
    {
        public Shop(string name, IItemsRepository<T> itemsRepository, bool isEnabled)
        {
            Name = name;
            ItemsRepository = itemsRepository;
            IsEnabled = isEnabled;
        }
        public string Name { get; private set; }
        public IItemsRepository<T> ItemsRepository { get; private set; }
        public bool IsEnabled { get; set; }
    }
}
