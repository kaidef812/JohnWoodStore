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
using JohnWoodStore.Model;
using JohnWoodStore.ViewModel;

namespace JohnWoodStore.View
{
    /// <summary>
    /// Логика взаимодействия для CatalogWindow.xaml
    /// </summary>
    public partial class CatalogWindow : Window
    {
        public CatalogWindow()
        {
            InitializeComponent();
            DataContext = new CatalogViewModel();
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ((dynamic)this.DataContext).ApplyFilters();
        }

        private void ComboBox_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            ((dynamic)this.DataContext).ApplyCategories();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if ((sender as TextBox).Text == "")
                ((dynamic)this.DataContext).InitializeProductList();
        }

        private void ProductGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ProductInDataGrid selectedProduct = (ProductInDataGrid)ProductGrid.SelectedItem;
            ((dynamic)this.DataContext).AddInCart(selectedProduct);
        }
    }
}
