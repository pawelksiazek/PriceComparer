using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using PriceComparer.App.Commands;
using PriceComparer.BusinessLayer.Models;

namespace PriceComparer.App.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        #region MainWindowHeader

        public string HeaderTitle => "Porównywarka cen";
        public string HeaderDescription => "Wyszukaj produkt w celu znalezienia oferty z najniższą ceną spośród dostępnych sklepów internetowych";

        #endregion

        public MainWindowViewModel()
        {
            GetAvailableCategories();
        }


        #region Categories

        private void GetAvailableCategories()
        {
            AvailableCategories = new Dictionary<int, string>
            {
                {0, "Książki" }
            };

            SelectedCategoryId = AvailableCategories.Select(x => x.Key).First();
        }

        public Dictionary<int, string> AvailableCategories { get; set; }

        #endregion


        public int SelectedCategoryId { get; set; }

        public string ProductNameToSearch { get; set; }


        #region SearchProductCommand

        private ICommand _searchProductCommand;
        public ICommand SearchProductCommand
        {
            get
            {
                return _searchProductCommand ?? (_searchProductCommand = new CommandHandler(() => SearchProduct(), _canExecuteSearchProductCommand));
            }
        }
        private bool _canExecuteSearchProductCommand = true;

        public void SearchProduct() => ProductsFound = new List<Book>
        {
            new Book {Isbn = 111, Title = "Book1", Author = "Author1", Price = 10},
            new Book {Isbn = 222, Title = "Book2", Author = "Author2", Price = 20},
            new Book {Isbn = 333, Title = "Book3", Author = "Author3", Price = 30}
        };

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
