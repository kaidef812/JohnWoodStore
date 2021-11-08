using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JohnWoodStore.View;
using JohnWoodStore.Model;
using JohnWoodStore.Model.Database;
using System.Security;
using System.Windows;

namespace JohnWoodStore.ViewModel
{
    class RegistrationViewModel
    {
        private WindowFunctions _windowFunctions;

        public RegistrationViewModel()
        {
            _windowFunctions = new WindowFunctions();
            UserTypeName = new List<string>();
            InitializeData();
        }

        #region Initialize data
        public List<string> UserTypeName { get; set; }

        private void InitializeData()
        {
            UserTypeName.Add("Физическое");
            UserTypeName.Add("Юридическое");
            OrganizationName = "";
        }
        #endregion

        #region Registrate Info
        public string Login { get; set; }
        public SecureString PasswordSecure { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public string ChoosedType { get; set; }
        public string OrganizationName { get; set; }
        public string OrganizationAddress { get; set; }
        public int UNP { get; set; }
        #endregion

        #region Try register
        private RelayCommand _register;
        public RelayCommand Register => TryRegister();
        private RelayCommand TryRegister()
        {
            return _register ?? new RelayCommand(obj =>
            {
                RegistrateNewUser();
            });
        }
        #endregion

        #region Registrate new user
        private string _passwordDecrypted;
        private void RegistrateNewUser()
        {
            _passwordDecrypted = new System.Net.NetworkCredential(string.Empty, PasswordSecure).Password;
            if (IsNotEmpty() == false)
            {
                MessageBox.Show("Заполните все поля.");
            }
            else if (Validator.IsValidEmail(Email) == false)
            {
                MessageBox.Show("Неправильный формат почты!");
            }
            else if (Validator.IsValidPhoneNumber(PhoneNumber) == false)
            {
                MessageBox.Show("Введите правильный номер телефона!");
            }
            else if(Validator.IsNotEnableLogin(Login) == true)
            {
                MessageBox.Show("Логин занят!");
            }
            else
            {
                using (StoreContext db = new StoreContext())
                {
                    var newUser = CreateNewUser();
                    db.Users.Add(newUser);
                    db.SaveChanges();
                }
                using (StoreContext db = new StoreContext())
                {
                    var userId = db.Users.ToList().Find(x => x.Login == Login).Id;
                    var newUserType = CreateNewUserType(userId);
                    db.UserTypes.Add(newUserType);
                    db.SaveChanges();
                }
                using (StoreContext db = new StoreContext())
                {
                    var userId = db.Users.ToList().Find(x => x.Login == Login).Id;
                    var newCart = CreateNewCart(userId);
                    db.Carts.Add(newCart);
                    db.SaveChanges();
                }
                OpenPreviosWindow();
            }
        }

        private User CreateNewUser()
        {
            User newUser = new User()
            {
                Login = Login,
                Password = _passwordDecrypted,
                Emal = Email,
                FullName = FullName,
                PhoneNumber = PhoneNumber,
                OrganizationName = OrganizationName,
                OrganizationAddress = OrganizationAddress,
                UNP = UNP
            };
            return newUser;
        }

        private UserType CreateNewUserType(int userId)
        {
            UserType newUserType = new UserType()
            {
                Name = ChoosedType,
                UserId = userId
            };
            return newUserType;
        }

        private Cart CreateNewCart(int userId)
        {
            Cart newCart = new Cart()
            {
                UserId = userId
            };
            return newCart;
        }

        private bool IsNotEmpty()
        {
            if (string.IsNullOrEmpty(Login) || string.IsNullOrEmpty(_passwordDecrypted) || string.IsNullOrEmpty(Email) ||
                string.IsNullOrEmpty(FullName) || string.IsNullOrEmpty(PhoneNumber) || string.IsNullOrEmpty(ChoosedType))
                return false;
            else if (ChoosedType == "Юридическое" && (string.IsNullOrEmpty(OrganizationName) || string.IsNullOrEmpty(OrganizationAddress)))
                return false;
            else
                return true;
        }
        #endregion

        #region Cancel
        private RelayCommand _cancel;
        public RelayCommand Cancel => CancelOperation();
        private RelayCommand CancelOperation()
        {
            return _cancel ?? new RelayCommand(obj =>
            {
                OpenPreviosWindow();
            });
        }
        #endregion
        private void OpenPreviosWindow()
        {
            MainWindow mainWindow = new MainWindow();
            _windowFunctions.OpenWindow(mainWindow);
        }
    }
}
