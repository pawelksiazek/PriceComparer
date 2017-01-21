using System.Collections.Generic;
using Common.DTO.BusinessModels;
using PriceComparer.BusinessLayer.Settings;

namespace PriceComparer.BusinessLayer.Interfaces
{
    public interface IComparerSettingsProvider<T> where T : Item<T>
    {
        ComparerSettings<T> ComparerSettings { get; set; }
        Dictionary<int, string> AvailableCategories { get; set; }
    }
}
