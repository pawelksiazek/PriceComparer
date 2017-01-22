using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Common.DTO.BusinessModels;
using PriceComparer.App.Commands;
using PriceComparer.BusinessLayer.Comparers;
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
        private readonly IItemsComparer<Book> _itemsComparer;

        public MainWindowViewModel()
        {
            _comparerSettingsProvider = ComparerSettingsProvider<Book>.Instance;
            _searchProvider = new SearchProvider();
            _itemsComparer = new ItemsComparer<Book>();

            GetAvailableShops();
        }

        private void GetAvailableShops()
        {
            AvailableShops = new ObservableCollection<Shop<Book>>();
            foreach (var shop in _comparerSettingsProvider.ComparerSettings.AvailableShops)
            {
                AvailableShops.Add(shop);
            }
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

        public ObservableCollection<Shop<Book>> AvailableShops { get; set; }


        #region SearchItemByNameCommand

        private ICommand _searchItemByNameCommand;
        public ICommand SearchItemByNameCommand
        {
            get
            {
                return _searchItemByNameCommand ?? (_searchItemByNameCommand = new CommandHandler(() => SearchItemByName(), _canExecuteSearchItemCommand));
            }
        }
        private bool _canExecuteSearchItemCommand = true;

        public void SearchItemByName()
        {
            ItemsFound = _searchProvider.SearchItemsByName(ProductNameToSearch);
            SelectedItem = null;
            ItemsFoundById = null;
            BestBuyItems = null;
        }

        #endregion


        private List<Book> _itemsFound;
        public List<Book> ItemsFound
        {
            get { return _itemsFound; }
            set
            {
                _itemsFound = value;
                OnPropertyChanged();
            }
        }

        private Book _selectedItem;
        public Book SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                _selectedItem = value;
                if (_selectedItem?.Ean != null && _selectedItem.Price != null)
                {
                    ItemsFoundById = _searchProvider.GetItemsById(_selectedItem.Ean);
                    if (ItemsFoundById != null)
                        BestBuyItems = ItemsFoundById.Count > 1 ? _itemsComparer.GetCheapestItems(ItemsFoundById) : ItemsFoundById;
                }
                else
                {
                    ItemsFoundById = null;
                    BestBuyItems = null;
                }
            }
        }

        private List<Book> _itemsFoundById;
        public List<Book> ItemsFoundById
        {
            get { return _itemsFoundById; }
            set
            {
                _itemsFoundById = value;
                OnPropertyChanged();
            }
        }

        private List<Book> _bestBuyItems;
        public List<Book> BestBuyItems
        {
            get { return _bestBuyItems; }
            set
            {
                _bestBuyItems = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(IsBestBuyOfferAvailable));
            }
        }

        public bool IsBestBuyOfferAvailable => BestBuyItems != null;


        #region GoToBestBuyOfferCommand

        private ICommand _goToBestBuyOfferCommand;
        public ICommand GoToBestBuyOfferCommand
        {
            get
            {
                return _goToBestBuyOfferCommand ?? (_goToBestBuyOfferCommand = new CommandHandler(() => GoToBestBuyOffer(), _canExecuteGoToBestBuyOfferCommand));
            }
        }
        private bool _canExecuteGoToBestBuyOfferCommand = true;

        public void GoToBestBuyOffer()
        {
            foreach (var bestBuyItem in BestBuyItems)
            {
                System.Diagnostics.Process.Start(bestBuyItem.Url);
            }
        }

        #endregion
    }
}
