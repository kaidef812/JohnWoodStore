using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JohnWoodStore.View;
using JohnWoodStore.Model;
using JohnWoodStore.Model.Database;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Windows;

namespace JohnWoodStore.ViewModel
{
    class CatalogViewModel : INotifyPropertyChanged
    {
        public static int UserId;
        private WindowFunctions _windowFunctions;
        public CatalogViewModel()
        {
            Filters = new List<string>();
            CategoriesName = new List<string>();
            Products = new ObservableCollection<ProductInDataGrid>();
            SelectedProducts = new ObservableCollection<SelectedProduct>();
            _windowFunctions = new WindowFunctions();

            InitializeData();

            InitializeProductList();
        }

        private void InitializeData()
        {
            Filters.Add("По цене возр");
            Filters.Add("По цене убыв");
            Filters.Add("По названию возр");
            Filters.Add("По названию убыв");
            Filters.Add("Без фильтра");

            using (StoreContext db = new StoreContext())
            {
                var allCategories = db.Categories;
                foreach (var category in allCategories)
                {
                    CategoriesName.Add(category.Name);
                }
                CategoriesName.Add("Все");
            }
        }

        #region Category and Filter
        public List<string> Filters { get; set; }
        public List<string> CategoriesName { get; set; }

        public string SelectedFilterName { get; set; }
        public string SelectedCategoryName { get; set; }

        public void ApplyFilters()
        {
            if (string.IsNullOrEmpty(SelectedFilterName) == false)
            {
                using (StoreContext db = new StoreContext())
                {
                    if (SelectedFilterName != Filters.Last())
                    {
                        if (SelectedFilterName == Filters[0])
                        {
                            var sortedProducts = new List<ProductInDataGrid>();
                            foreach (var product in Products)
                            {
                                sortedProducts.Add(product);
                            }
                            sortedProducts = sortedProducts.OrderBy(x => x.Price).ToList();
                            Products.Clear();
                            foreach (var product in sortedProducts)
                            {
                                Products.Add(product);
                            }
                            NotifyPropertyChanged(nameof(Products));
                        }
                        else if(SelectedFilterName == Filters[1])
                        {
                            var sortedProducts = new List<ProductInDataGrid>();
                            foreach (var product in Products)
                            {
                                sortedProducts.Add(product);
                            }
                            sortedProducts = sortedProducts.OrderByDescending(x => x.Price).ToList();
                            Products.Clear();
                            foreach (var product in sortedProducts)
                            {
                                Products.Add(product);
                            }
                            NotifyPropertyChanged(nameof(Products));
                        }
                        else if (SelectedFilterName == Filters[2])
                        {
                            var sortedProducts = new List<ProductInDataGrid>();
                            foreach (var product in Products)
                            {
                                sortedProducts.Add(product);
                            }
                            sortedProducts = sortedProducts.OrderBy(x => x.Name).ToList();
                            Products.Clear();
                            foreach (var product in sortedProducts)
                            {
                                Products.Add(product);
                            }
                            NotifyPropertyChanged(nameof(Products));
                        }
                        else if (SelectedFilterName == Filters[3])
                        {
                            var sortedProducts = new List<ProductInDataGrid>();
                            foreach (var product in Products)
                            {
                                sortedProducts.Add(product);
                            }
                            sortedProducts = sortedProducts.OrderByDescending(x => x.Name).ToList();
                            Products.Clear();
                            foreach (var product in sortedProducts)
                            {
                                Products.Add(product);
                            }
                            NotifyPropertyChanged(nameof(Products));
                        }
                    }
                    else
                    {
                        InitializeProductList();
                        NotifyPropertyChanged(nameof(Products));
                    }
                }
            }
        }
        public void ApplyCategories()
        {
            if (string.IsNullOrEmpty(SelectedCategoryName) == false)
            {
                using (StoreContext db = new StoreContext())
                {
                    if (SelectedCategoryName == CategoriesName.Last())
                    {
                        InitializeProductList();
                    }
                    else
                    {
                        var products = db.Products.ToList();
                        var category = db.Categories.ToList().Find(x => x.Name == SelectedCategoryName);
                        if (category != null)
                        {
                            Products.Clear();
                            var categoryId = category.Id;
                            foreach (var product in products)
                            {
                                if (product.CategoryId == categoryId)
                                    Products.Add(new ProductInDataGrid(product));
                            }
                            NotifyPropertyChanged(nameof(Products));
                        }
                    }
                }
            }
        }
        #endregion

        #region Open user info window
        private RelayCommand _showUserInfo;
        public RelayCommand ShowUserInfo => OpenUserInfoWindow();
        private RelayCommand OpenUserInfoWindow()
        {
            return _showUserInfo ?? new RelayCommand(obj =>
            {
                UserInfoWindow userInfoWindow = new UserInfoWindow();
                _windowFunctions.OpenDialogWindow(userInfoWindow);
            });
        }
        #endregion

        #region Products
        public ObservableCollection<ProductInDataGrid> Products { get; private set; }
        public static ObservableCollection<SelectedProduct> SelectedProducts { get; set; }

        public void InitializeProductList()
        {
            Products.Clear();
            using (StoreContext db = new StoreContext())
            {
                var products = db.Products;
                foreach (var product in products)
                {
                    Products.Add(new ProductInDataGrid(product));
                }
            }
        }

        #endregion

        #region Search
        private RelayCommand _search;
        public RelayCommand Search => SearchProducts();
        private RelayCommand SearchProducts()
        {
            return _search ?? new RelayCommand(obj =>
            {
                TrySearchProducts(obj as string);
            });
        }
        private void TrySearchProducts(string searchingText)
        {
            if (string.IsNullOrEmpty(searchingText) == false)
            {
                using (StoreContext db = new StoreContext())
                {
                    var products = db.Products.ToList();
                    Products.Clear();
                    products = products.Where(x => x.Name.Contains(searchingText) == true).ToList();
                    foreach (var product in products)
                    {
                        Products.Add(new ProductInDataGrid(product));
                    }
                    NotifyPropertyChanged(nameof(Products));
                }
            }
        }
        #endregion

        #region Add in cart product 
        public void AddInCart(ProductInDataGrid selectedItem)
        {
            if (selectedItem != null)
            {
                using (StoreContext db = new StoreContext())
                {
                    var product = db.Products.ToList().Find(x => x.Id == selectedItem.GetId());
                    if (product != null)
                    {
                        var selectedProduct = new SelectedProduct(product);
                        if (SelectedProducts.ToList().Exists(x => x.GetId() == selectedProduct.GetId()) == false)
                        {
                            SelectedProducts.Add(selectedProduct);
                        }
                        else
                        {
                            var productToIncrease = SelectedProducts.ToList().Find(x => x.GetId() == selectedProduct.GetId());
                            var buffer = new List<SelectedProduct>();
                            foreach (var pr in SelectedProducts)
                            {
                                buffer.Add(pr);
                            }
                            buffer.Find(x => x == productToIncrease).Count++;
                            SelectedProducts.Clear();
                            foreach (var pr in buffer)
                            {
                                SelectedProducts.Add(pr);
                            }
                        }
                        NotifyPropertyChanged(nameof(SelectedProducts));
                    }
                }
            }
        }
        #endregion

        #region OpenCart
        private RelayCommand _openCart;
        public RelayCommand OpenCart => OpenCartWindow();
        private RelayCommand OpenCartWindow()
        {
            return _openCart ?? new RelayCommand(obj =>
            {
                if (SelectedProducts.Count == 0)
                {
                    MessageBox.Show("Корзина пуста.");
                }
                else
                {
                    CartWindow cartWindow = new CartWindow();
                    _windowFunctions.OpenDialogWindow(cartWindow);
                    if (cartWindow.DialogResult == true)
                        SelectedProducts.Clear();
                }
            });
        }
        #endregion

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
