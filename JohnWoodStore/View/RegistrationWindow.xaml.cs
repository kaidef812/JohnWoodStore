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
    /// Логика взаимодействия для RegistrationWindow.xaml
    /// </summary>
    public partial class RegistrationWindow : Window
    {
        public RegistrationWindow()
        {
            InitializeComponent();
            DataContext = new RegistrationViewModel();
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox box = sender as ComboBox;
            ((dynamic)this.DataContext).ChoosedType = box.SelectedItem.ToString();
            var uridFace = ((dynamic)this.DataContext).UserTypeName[1];
            if(box.SelectedItem.ToString() == uridFace)
            {
                inputOrganizationName.Visibility = Visibility.Visible;
                textOrganizationName.Visibility = Visibility.Visible;
                inputOrganizationAddress.Visibility = Visibility.Visible;
                textOrganizationAddress.Visibility = Visibility.Visible;
                inputUNP.Visibility = Visibility.Visible;
                textUNP.Visibility = Visibility.Visible;
            }
            else
            {
                inputOrganizationName.Visibility = Visibility.Hidden;
                textOrganizationName.Visibility = Visibility.Hidden;
                inputOrganizationAddress.Visibility = Visibility.Hidden;
                textOrganizationAddress.Visibility = Visibility.Hidden;
                inputUNP.Visibility = Visibility.Hidden;
                textUNP.Visibility = Visibility.Hidden;
            }
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (this.DataContext != null)
                ((dynamic)this.DataContext).PasswordSecure = ((PasswordBox)sender).SecurePassword;
        }
    }
}
