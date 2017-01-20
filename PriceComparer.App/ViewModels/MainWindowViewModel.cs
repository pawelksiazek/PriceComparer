using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using PriceComparer.App.Commands;
using PriceComparer.BusinessLayer.Interfaces;
using PriceComparer.BusinessLayer.Models;
using PriceComparer.BusinessLayer.Providers;
using PriceComparer.BusinessLayer.Settings;

namespace PriceComparer.App.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        #region MainWindowHeader

        public string HeaderTitle => "Porównywarka cen";
        public string HeaderDescription => "Wyszukaj produkt w celu znalezienia oferty z najniższą ceną spośród dostępnych sklepów internetowych";

        #endregion

        private readonly IComparerSettingsProvider<Book> _comparerSettingsProvider;
        private readonly ISearchProvider _searchProvider;

        public MainWindowViewModel()
        {
            _comparerSettingsProvider = ComparerSettingsProvider<Book>.Instance;
            _searchProvider = new SearchProvider();
        }

        public Dictionary<int, string> AvailableCategories => _comparerSettingsProvider.AvailableCategories;

        public int SelectedCategoryId
        {
            get { return _comparerSettingsProvider.ComparerSettings.SelectedCategoryId; }
            set { _comparerSettingsProvider.ComparerSettings.SelectedCategoryId = value; }
        }

        private string _productNameToSearch;
        public string ProductNameToSearch
        {
            get { return _productNameToSearch; }
            set
            {
                _productNameToSearch = value;
                OnPropertyChanged(nameof(IsProductNameProvided));
            }
        }

        public bool IsProductNameProvided => ProductNameToSearch != null;

        #region SearchProductByNameCommand

        private ICommand _searchProductByNameCommand;
        public ICommand SearchProductByNameCommand
        {
            get
            {
                return _searchProductByNameCommand ?? (_searchProductByNameCommand = new CommandHandler(() => SearchProductByName(), _canExecuteSearchProductCommand));
            }
        }
        private bool _canExecuteSearchProductCommand = true;

        public void SearchProductByName() => ProductsFound = _searchProvider.SearchItemsByName(ProductNameToSearch);

        #endregion


        private List<Book> _productsFound;

        public List<Book> ProductsFound
        {
            get { return _productsFound; }
            set
            {
                _productsFound = value;
                OnPropertyChanged();
            }
        }

        public string SelectedProduct { get; set; }
    }
}
