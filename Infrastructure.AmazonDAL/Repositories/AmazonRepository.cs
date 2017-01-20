using System.Collections.Generic;
using Infrastructure.Common.Interfaces;

namespace Infrastructure.AmazonDAL.Repositories
{
    public class AmazonRepository<T> : IItemsRepository<T>
    {
        public List<T> SearchItemsByName(string itemName)
        {
            throw new System.NotImplementedException();
        }

        public T GetItemById(int itemId)
        {
            throw new System.NotImplementedException();
        }
    }
}
