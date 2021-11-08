using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace JohnWoodStore.Model
{
    class WindowFunctions
    {
        public void OpenWindow(Window window)
        {
            Application.Current.MainWindow.Close();
            Application.Current.MainWindow = window;
            window.Show();
        }

        public void OpenDialogWindow(Window window)
        {
            window.ShowDialog();
        }

        public void CloseDialogWindow(Window window)
        {
            window.DialogResult = false;
        }
    }
}
