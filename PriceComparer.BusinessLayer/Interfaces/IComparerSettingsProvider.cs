using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PriceComparer.BusinessLayer.Models;
using PriceComparer.BusinessLayer.Settings;

namespace PriceComparer.BusinessLayer.Interfaces
{
    public interface IComparerSettingsProvider<T> where T : Product
    {
        ComparerSettings<T> ComparerSettings { get; set; }
        Dictionary<int, string> AvailableCategories { get; set; }
    }
}
