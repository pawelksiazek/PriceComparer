using System.Collections.Generic;
using Infrastructure.Common.Interfaces;
using PriceComparer.BusinessLayer.Models;

namespace PriceComparer.BusinessLayer.Managers
{
    public class ProductsManager<T> where T : Product
    {
        private readonly IItemsRepository<T> _itemsRepository;

        public ProductsManager(IItemsRepository<T> itemsRepository)
        {
            _itemsRepository = itemsRepository;
        }

        public List<T> GetProductsByName(string productName)
        {
            return new List<T>();
        }
    }
}
