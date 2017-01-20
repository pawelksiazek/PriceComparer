using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PriceComparer.BusinessLayer.Models;

namespace PriceComparer.BusinessLayer.Settings
{
    public class ComparerSettings<T> where T : Product
    {
        public int SelectedCategoryId { get; set; }
        public List<Shop<T>> AvailableShops { get; set; }
    }
}
