using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PriceComparer.BusinessLayer.Models;

namespace PriceComparer.BusinessLayer.Interfaces
{
    public interface ISearchProvider
    {
        List<Book> SearchItemsByName(string itemName);
    }
}
