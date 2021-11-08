using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using JohnWoodStore.View;
using JohnWoodStore.Model;
using JohnWoodStore.Model.Database;

namespace JohnWoodStore.ViewModel
{
    class UserInfoViewModel
    {
        private UserInfoWindow _currentWindow;
        private WindowFunctions _windowFunctions;

        public UserInfoViewModel(UserInfoWindow window)
        {
            _currentWindow = window;
            _windowFunctions = new WindowFunctions();
            InitializeUserInfo();
        }

        #region User info
        public string Login { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public string UserType { get; set; }
        public string OrganizationName { get; set; }
        public string OrganizationAddress { get; set; }
        public int UNP { get; set; }

        private void InitializeUserInfo()
        {
            using(StoreContext db = new StoreContext())
            {
                var user = db.Users.Find(CatalogViewModel.UserId);
                var userType = db.UserTypes.ToList().Find(x => x.UserId == user.Id).Name;

                Login = user.Login;
                Email = user.Emal;
                FullName = user.FullName;
                PhoneNumber = user.PhoneNumber;
                UserType = userType;
                OrganizationName = user.OrganizationName;
                OrganizationAddress = user.OrganizationAddress;
                UNP = user.UNP;
            }
            if(UserType == "Юридическое")
            {
                _currentWindow.ShowOrganizationInfo();
            }
        }
        #endregion

        #region Cancel
        private RelayCommand _cancel;
        public RelayCommand Cancel => GoBack();
        private RelayCommand GoBack()
        {
            return _cancel ?? new RelayCommand(obj =>
            {
                _windowFunctions.CloseDialogWindow(_currentWindow);
            });
        }
        #endregion

        #region LogOut
        private RelayCommand _logOut;
        public RelayCommand LogOut => UserLogOut();
        private RelayCommand UserLogOut()
        {
            return _logOut ?? new RelayCommand(obj =>
            {
                OpenLoginWindow();
            });
        }
        private void OpenLoginWindow()
        {
            MainWindow mainWindow = new MainWindow();
            _windowFunctions.CloseDialogWindow(_currentWindow);
            _windowFunctions.OpenWindow(mainWindow);
        }
        #endregion
    }
}
