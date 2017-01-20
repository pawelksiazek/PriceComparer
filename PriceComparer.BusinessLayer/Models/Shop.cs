using Infrastructure.Common.Interfaces;

namespace PriceComparer.BusinessLayer.Models
{
    public class Shop<T> where T : Product
    {
        public Shop(string name, IItemsRepository<T> itemsRepository)
        {
            Name = name;
            ItemsRepository = itemsRepository;
        }
        public string Name { get; private set; }
        public IItemsRepository<T> ItemsRepository { get; private set; }
    }
}
