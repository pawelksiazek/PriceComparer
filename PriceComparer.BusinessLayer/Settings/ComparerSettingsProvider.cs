using System.Collections.Generic;
using System.Linq;
using Common.DTO.BusinessModels;
using Infrastructure.AmazonDAL.Repositories;
using Infrastructure.ApressDAL.Repositories;
using PriceComparer.BusinessLayer.Interfaces;
using PriceComparer.BusinessLayer.Models;

namespace PriceComparer.BusinessLayer.Settings
{
    public class ComparerSettingsProvider<T> : IComparerSettingsProvider<T> where T : Item<T>, new()
    {
        private static ComparerSettingsProvider<T> _instance;
        public static ComparerSettingsProvider<T> Instance => _instance ?? (_instance = new ComparerSettingsProvider<T>());

        private ComparerSettingsProvider()
        {
            GetAvailableCategories();
            SetDefaultComparerSettings();
        }

        public ComparerSettings<T> ComparerSettings { get; set; }

        public Dictionary<int, string> AvailableCategories { get; set; }

        private void GetAvailableCategories()
        {
            AvailableCategories = new Dictionary<int, string>
            {
                {0, "Książki" }
            };
        }

        public void SetDefaultComparerSettings()
        {
            ComparerSettings = new ComparerSettings<T>
            {
                SelectedCategoryId = AvailableCategories.Select(x => x.Key).First(),
                AvailableShops = new List<Shop<T>>
                {
                    new Shop<T>("Amazon", new AmazonRepository<T>(), true),
                    new Shop<T>("Apress", new ApressRepository<T>(), true)
                }
            };
        }
    }
}
