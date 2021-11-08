using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using JohnWoodStore.View;
using JohnWoodStore.Model;
using JohnWoodStore.Model.Database;

namespace JohnWoodStore.ViewModel
{
    class LoginViewModel
    {
        private WindowFunctions _windowFunctions;

        public LoginViewModel()
        {
            _windowFunctions = new WindowFunctions();
        }

        #region Login Info
        public string LoginText { get; set; }
        public SecureString PasswordSecureText { get; set; }
        #endregion

        #region Try login
        private RelayCommand _login;
        public RelayCommand Login => TryLogin();
        private RelayCommand TryLogin()
        {
            return _login ?? new RelayCommand(obj =>
            {
                UserTryLogin();
            });
        }
        private void UserTryLogin()
        {
            using(StoreContext db = new StoreContext())
            {
                var findedUser = db.Users.ToList().Find(x => x.Login == LoginText);
                if (findedUser != null)
                {
                    var decryptedPassword = new System.Net.NetworkCredential(string.Empty, PasswordSecureText).Password;
                    if (findedUser.Password == decryptedPassword)
                    {
                        CatalogViewModel.UserId = findedUser.Id;
                        CatalogWindow catalogWindow = new CatalogWindow();
                        _windowFunctions.OpenWindow(catalogWindow);
                    }
                    else
                        MessageBox.Show("Неверный пароль!");
                }
                else
                    MessageBox.Show("Неверный логин!");
            }
        }
        #endregion

        #region Registrate
        private RelayCommand _register;
        public RelayCommand Register => RegistrateNewUser();
        private RelayCommand RegistrateNewUser()
        {
            return _register ?? new RelayCommand(obj =>
            {
                OpenRegistrationWindow();
            });
        }
        private void OpenRegistrationWindow()
        {
            RegistrationWindow window = new RegistrationWindow();
            _windowFunctions.OpenWindow(window);
        }
        #endregion
    }
}
