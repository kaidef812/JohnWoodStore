using JohnWoodStore.Model;
using JohnWoodStore.Model.Database;
using JohnWoodStore.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace JohnWoodStore.ViewModel
{
    class PayViewModel : INotifyPropertyChanged
    {
        private Window _currentWindow;
        private WindowFunctions _windowFunctions;
        private Order _currentOrder;

        public PayViewModel(Window window)
        {
            SetCurrentOrder();
            _windowFunctions = new WindowFunctions();
            _currentWindow = window;
            PaymentMethods = new List<string>();
            InitializePaymentMethods();
            SetTotalPrice();
        }

        private void SetCurrentOrder()
        {
            using (StoreContext db = new StoreContext())
            {
                var userId = CatalogViewModel.UserId;
                var userCart = db.Carts.ToList().Find(x => x.UserId == userId);
                if (userCart != null)
                {
                    var order = db.Orders.ToList().Where(x => x.CartId == userCart.Id).LastOrDefault();
                    if (order != null)
                    {
                        _currentOrder = order;
                    }
                }
            }
        }

        #region Pay
        private RelayCommand _pay;
        public RelayCommand Pay => PayOrder();
        private RelayCommand PayOrder()
        {
            return _pay ?? new RelayCommand(obj =>
            {
                PayCurrentOrder();
            });
        }

        public decimal TotalCost { get; set; }

        private void PayCurrentOrder()
        {
            if (string.IsNullOrEmpty(SelectedMethod) == true)
            {
                MessageBox.Show("Выберите способ оплаты!");
            }
            else
            {
                User user;
                UserType userType;
                using (StoreContext db = new StoreContext())
                {
                    user = db.Users.ToList().Find(x => x.Id == CatalogViewModel.UserId);
                    userType = db.UserTypes.ToList().Find(x => x.UserId == user.Id);
                }
                CreatePaymentMethod();
                CreateAccountingForOrder();
                CreateCheck();

                if (userType.Name == "Физическое")
                    PrintCheckPhysUserType();
                else
                    PrintCheckUridUserType();

                MessageBox.Show("Заказ успешно оформлен!");
                _currentWindow.DialogResult = true;
            }
        }

        private void CreatePaymentMethod()
        {
            if (string.IsNullOrEmpty(SelectedMethod) == false)
            {
                using (StoreContext db = new StoreContext())
                {
                    PaymentMethod newPaymentMethod = new PaymentMethod()
                    {
                        Name = SelectedMethod,
                        OrderId = _currentOrder.Id
                    };
                    db.PaymentMethods.Add(newPaymentMethod);
                    db.SaveChanges();
                }
            }
        }

        private void CreateAccountingForOrder()
        {
            using (StoreContext db = new StoreContext())
            {
                AccountingForOrders newAccountingForOrders = new AccountingForOrders()
                {
                    OrderId = _currentOrder.Id
                };
                db.AccountingForOrders.Add(newAccountingForOrders);
                db.SaveChanges();
            }
        }

        private void CreateCheck()
        {
            using (StoreContext db = new StoreContext())
            {
                var number = new Random().Next(100, 10000);
                while (db.Checks.ToList().Exists(x => x.Number == number) == true)
                {
                    number = new Random().Next(100, 10000);
                }
                Check newCheck = new Check()
                {
                    Number = number,
                    TotalPrice = TotalCost,
                    DateOfCheck = DateTime.Now,
                    OrderId = _currentOrder.Id
                };
                db.Checks.Add(newCheck);
                db.SaveChanges();
            }
        }

        private void SetTotalPrice()
        {
            decimal totalPrice = 0;
            using (StoreContext db = new StoreContext())
            {
                var products = db.Products.ToList();
                foreach (var selectedProduct in CatalogViewModel.SelectedProducts)
                {
                    totalPrice += products.Find(x => x.Id == selectedProduct.GetId()).Price * selectedProduct.Count;
                }
            }
            TotalCost = totalPrice;
            NotifyPropertyChanged(nameof(TotalCost));
        }

        private void PrintCheckPhysUserType()
        {
            using (StoreContext db = new StoreContext())
            {
                var check = db.Checks.ToList().Find(x => x.OrderId == _currentOrder.Id);
                if (check != null)
                {
                    var user = db.Users.ToList().Find(x => x.Id == CatalogViewModel.UserId);
                    CheckPdfProvider checkPdf = new CheckPdfProvider();
                    checkPdf.AddTitle("ЧЕК");
                    checkPdf.AddFooter("");
                    checkPdf.AddParagraph("ООО \"JohnWoodStore\"");
                    checkPdf.AddParagraph($"Кассовый чек № {check.Number}");
                    checkPdf.AddFooter("");
                    checkPdf.AddParagraph($"К оплате: {check.TotalPrice}");
                    checkPdf.AddParagraph($"Дата оплаты: {check.DateOfCheck}");
                    checkPdf.AddFooter("");
                    checkPdf.AddTable();
                    checkPdf.Save("Check " + check.Number.ToString());
                }
            }
        }

        private void PrintCheckUridUserType()
        {
            using (StoreContext db = new StoreContext())
            {
                var check = db.Checks.ToList().Find(x => x.OrderId == _currentOrder.Id);
                if (check != null)
                {
                    var user = db.Users.ToList().Find(x => x.Id == CatalogViewModel.UserId);
                    CheckPdfProvider checkPdf = new CheckPdfProvider();
                    checkPdf.AddFooter("");
                    checkPdf.AddParagraph("ООО \"JohnWoodStore\"");
                    checkPdf.AddParagraph($"Кассовый чек № {check.Number}");
                    checkPdf.AddFooter("");
                    checkPdf.AddParagraph($"К оплате: {check.TotalPrice}");
                    checkPdf.AddParagraph($"Дата оплаты: {check.DateOfCheck}");
                    checkPdf.AddFooter("");
                    checkPdf.AddTable();
                    checkPdf.AddFooter("");
                    checkPdf.Save("Check " + check.Number.ToString());

                    var number = new Random().Next(1000000, 10000000); 
                    DocumentPdfProvider documentPdf = new DocumentPdfProvider();
                    documentPdf.AddTitle($"Договор N {_currentOrder.Number}");
                    documentPdf.AddParagraph("");
                    documentPdf.AddNameOrganizations($"{_currentOrder.DateOfOrdering} г. Минск");
                    documentPdf.AddParagraph("");
                    documentPdf.AddParagraph("");
                    documentPdf.AddParagraph($"ООО \"JohnWoodStore\" именуемый в дальнейшем \"Заказчик\", в лице директора Сидоров Адам Николаевич, " +
                        $"действующий на основании Устава, с одной стороны и индивидуальный предприниматель {user.FullName}, " +
                        $"именуемый в дальнейшем \"Подрядчик\", действующий на основании Свидетельства о регистрации N {number} от" +
                        $" {DateTime.Now.ToShortDateString()}, зарегистрированный Минским горисполкомом, с другой стороны, а вместе " +
                        $"именуемые \"Стороны\", руководствуясь положением о договорах подряда на выполнение проектных и изыскательских " +
                        $"работ, заключили настоящий договор о нижеследующем:");
                    documentPdf.AddParagraph("");
                    documentPdf.AddParagraph($"Подрядчик обязуется по заданию Заказчика осуществить доставку всех товаров, в количестве" +
                        $" {CatalogViewModel.SelectedProducts.Count}. Доставка осуществляется по адресу Заказчика, {user.OrganizationAddress}.");
                    documentPdf.AddParagraph("");
                    documentPdf.AddParagraph($"Стоимость по договору составляет {TotalCost} белорусских рублей, без НДС.");
                    documentPdf.AddParagraph("");
                    documentPdf.AddParagraph("Заказчик производит оплату 100% объема стоимости доставки и закупки товаров по договору.");
                    documentPdf.AddParagraph("");
                    documentPdf.AddParagraph("Заказчик обязан выплатить выполненный заказ согласно акта выполненных работ в течении 10-ти банковских дней" +
                        " с момента его подписания.");
                    documentPdf.AddParagraph("");
                    documentPdf.AddParagraph("");
                    documentPdf.AddParagraph("");
                    documentPdf.AddParagraph("");
                    documentPdf.AddParagraph("");
                    documentPdf.AddParagraph("");
                    documentPdf.AddParagraph("");
                    documentPdf.AddParagraph("");
                    documentPdf.AddParagraph("");
                    documentPdf.AddParagraph("");
                    documentPdf.AddParagraph("");
                    documentPdf.AddParagraph("");
                    documentPdf.AddParagraph("");
                    documentPdf.AddParagraph("\"ЗАКАЗЧИК\"");
                    documentPdf.AddParagraph($"{user.OrganizationName}");
                    documentPdf.AddParagraph($"г. Минск, {user.OrganizationAddress}");
                    documentPdf.AddParagraph($"ЗАО \"Альфа-Банк\"");
                    documentPdf.AddParagraph($"220013, г. Минск, ул. Сурганова, 43-47");
                    documentPdf.AddParagraph($"УНП {user.UNP}");
                    documentPdf.AddParagraph($"Email {user.Emal}");
                    documentPdf.AddParagraph($"Тел./моб. {user.PhoneNumber}");
                    documentPdf.AddParagraph("");
                    documentPdf.AddParagraph($"Директор {user.FullName}");
                    documentPdf.AddParagraph("_______________________________");
                    documentPdf.AddParagraph("");
                    documentPdf.AddParagraph("");
                    documentPdf.AddParagraph("\"ПОДРЯДЧИК\"");
                    documentPdf.AddParagraph($"ИП Сидоров Адам Николаевич");
                    documentPdf.AddParagraph($"г. Минск, Ленинская 47");
                    documentPdf.AddParagraph($"ЗАО \"Альфа-Банк\"");
                    documentPdf.AddParagraph($"220013, г. Минск, ул. Сурганова, 43-47");
                    documentPdf.AddParagraph($"УНП 1982637841");
                    documentPdf.AddParagraph($"Email johnWoodStore@gmail.com");
                    documentPdf.AddParagraph($"Тел./моб. +375298745296");
                    documentPdf.AddParagraph("");
                    documentPdf.AddParagraph($"Индивидуальный предприниматель Сидоров Адам Николаевич");
                    documentPdf.AddParagraph("_______________________________");
                    documentPdf.Save("Document " + check.Number.ToString());
                }
            }
        }
        #endregion

        #region Payment methods
        public List<string> PaymentMethods { get; set; }
        public string SelectedMethod { get; set; }

        private void InitializePaymentMethods()
        {
            PaymentMethods.Add("Карточкой");
            PaymentMethods.Add("Наличными");
        }
        #endregion

        #region Cancel
        private RelayCommand _cancel;
        public RelayCommand Cancel => CancelOperation();
        private RelayCommand CancelOperation()
        {
            return _cancel ?? new RelayCommand(obj =>
            {
                _windowFunctions.CloseDialogWindow(_currentWindow);
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
