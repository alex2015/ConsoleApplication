using System;
using System.Windows;
using System.Windows.Controls;

namespace WpfAppAllCode
{
    class MainWindow : Window
    {
        private Button btnExitApp = new Button();
        public MainWindow(string windowTitle, int height, int width)
        {
            // Сконфигурировать кнопку
            btnExitApp.Click += btnExitApp_Clicked;
            btnExitApp.Content = "Exit Application";
            btnExitApp.Height = 25;
            btnExitApp.Width = 100;

            this.AddChild(btnExitApp);

            // Сконфигурируем окно
            this.Title = windowTitle;
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            this.Height = height;
            this.Width = width;

            this.Closing += MainWindow_Closing;
            this.Closed += MainWindow_Closed;
        }

        private void btnExitApp_Clicked(object sender, RoutedEventArgs e)
        {
            if ((bool) Application.Current.Properties["GodMode"])
            {
                MessageBox.Show("Cheater!");
            }

            this.Close();
        }

        private void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            var result = MessageBox.Show("Do you want to close without saving?", "My App", MessageBoxButton.YesNo, MessageBoxImage.Warning);

            if (result == MessageBoxResult.No)
            {
                e.Cancel = true;
            }
        }

        private void MainWindow_Closed(object sender, EventArgs e)
        {
            MessageBox.Show("See ya!");
        }
    }
}
