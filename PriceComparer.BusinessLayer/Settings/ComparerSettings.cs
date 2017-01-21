using System.Collections.Generic;
using Common.DTO.BusinessModels;
using PriceComparer.BusinessLayer.Models;

namespace PriceComparer.BusinessLayer.Settings
{
    public class ComparerSettings<T> where T : Item<T>
    {
        public int SelectedCategoryId { get; set; }
        public List<Shop<T>> AvailableShops { get; set; }
    }
}
