using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using JohnWoodStore.ViewModel;

namespace JohnWoodStore.View
{
    /// <summary>
    /// Логика взаимодействия для UserInfoWindow.xaml
    /// </summary>
    public partial class UserInfoWindow : Window
    {
        public UserInfoWindow()
        {
            InitializeComponent();
            DataContext = new UserInfoViewModel(this);
        }

        public void ShowOrganizationInfo()
        {
            textOrganizationName.Visibility = Visibility.Visible;
            organizationName.Visibility = Visibility.Visible;
            textOrganizationAddress.Visibility = Visibility.Visible;
            organizationAddress.Visibility = Visibility.Visible;
            textUNP.Visibility = Visibility.Visible;
            UNP.Visibility = Visibility.Visible;
        }
    }
}
