using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JohnWoodStore.View;
using JohnWoodStore.Model;
using JohnWoodStore.Model.Database;
using System.ComponentModel;
using System.Windows;

namespace JohnWoodStore.ViewModel
{
    class CartViewModel : INotifyPropertyChanged
    {
        private WindowFunctions _windowFunctions;
        private Window _currentWindow;
        public CartViewModel(Window window)
        {
            _currentWindow = window;
            Products = new ObservableCollection<ProductInCart>();
            _windowFunctions = new WindowFunctions();
            SetProducts();
            SetTotalCost();
            CreateOrder();
        }

        #region Products from orders
        public ObservableCollection<ProductInCart> Products { get; set; }

        private void SetProducts()
        {
            Products.Clear();
            if (CatalogViewModel.SelectedProducts.Count != 0)
            {
                using (StoreContext db = new StoreContext())
                {
                    foreach (var selectedProduct in CatalogViewModel.SelectedProducts)
                    {
                        var product = db.Products.ToList().Find(x => x.Id == selectedProduct.GetId());
                        Products.Add(new ProductInCart(product, selectedProduct.Count));
                    }
                }
            }
        }
        #endregion

        #region Total cost
        public decimal TotalCost { get; set; }

        private void SetTotalCost()
        {
            TotalCost = 0;
            foreach (var cartProduct in Products)
            {
                TotalCost += cartProduct.Price;
            }
            NotifyPropertyChanged(nameof(TotalCost));
        }
        #endregion

        #region Create Delete Order
        private void CreateOrder()
        {
            using (StoreContext db = new StoreContext())
            {
                var user = db.Users.ToList().Find(x => x.Id == CatalogViewModel.UserId);
                if (user != null)
                {
                    var userCart = db.Carts.ToList().Find(x => x.UserId == user.Id);
                    if (userCart != null)
                    {
                        var randomNumber = new Random().Next(1000, 10000);
                        while (db.Orders.ToList().Exists(x => x.Number == randomNumber) == true)
                            randomNumber = new Random().Next(1000, 10000);

                        Order newOrder = new Order()
                        {
                            Number = randomNumber,
                            DateOfOrdering = DateTime.Now,
                            CartId = userCart.Id
                        };

                        db.Orders.Add(newOrder);
                        db.SaveChanges();
                    }
                }
            }
            CreateOrderProducts();
        }

        private void CreateOrderProducts()
        {
            if (CatalogViewModel.SelectedProducts.Count != 0)
            {
                using (StoreContext db = new StoreContext())
                {
                    var user = db.Users.ToList().Find(x => x.Id == CatalogViewModel.UserId);
                    if (user != null)
                    {
                        var userCart = db.Carts.ToList().Find(x => x.UserId == user.Id);
                        if (userCart != null)
                        {
                            var currentOrder = db.Orders.ToList().Where(x => x.CartId == userCart.Id).LastOrDefault();
                            foreach (var product in CatalogViewModel.SelectedProducts)
                            {
                                OrderProduct newOrderProduct = new OrderProduct()
                                {
                                    OrderId = currentOrder.Id,
                                    ProductId = product.GetId()
                                };
                                db.OrderProducts.Add(newOrderProduct);
                                db.SaveChanges();
                            }
                        }
                    }
                }
            }
        }

        private void CancelOrder()
        {
            using (StoreContext db = new StoreContext())
            {
                var user = db.Users.ToList().Find(x => x.Id == CatalogViewModel.UserId);
                if (user != null)
                {
                    var userCart = db.Carts.ToList().Find(x => x.UserId == user.Id);
                    if (userCart != null)
                    {
                        var orderToRemove = db.Orders.ToList().Where(x => x.CartId == userCart.Id).Last();
                        db.Orders.Remove(orderToRemove);
                        db.SaveChanges();
                    }
                }
            }
        }
        #endregion

        #region Pay Order
        private RelayCommand _pay;
        public RelayCommand Pay => PayOrder();
        private RelayCommand PayOrder()
        {
            return _pay ?? new RelayCommand(obj =>
            {
                OpenPayWindow();
            });
        }

        private void OpenPayWindow()
        {
            PayWindow payWindow = new PayWindow();
            _windowFunctions.OpenDialogWindow(payWindow);
            if(payWindow.DialogResult == true)
            {
                _currentWindow.DialogResult = true;
            }
        }
        #endregion

        #region Go back
        private RelayCommand _goBack;
        public RelayCommand GoBack => GoToPreviosWindow();
        private RelayCommand GoToPreviosWindow()
        {
            return _goBack ?? new RelayCommand(obj =>
            {
                OpenPreviosWindow();
            });
        }
        private void OpenPreviosWindow()
        {
            CancelOrder();
            _windowFunctions.CloseDialogWindow(_currentWindow);
        }
        #endregion

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
