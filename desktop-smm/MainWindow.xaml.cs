using System;
using System.IO;
using System.Windows;
using desktop_smm.Pages;
using desktop_smm.Services;

namespace desktop_smm
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            MainLoad.Navigate(new AuthPage());
        }

        async void FetchData()
        {
            try
            {
                await Store.FetchOrders();
                await Store.FetchTypes();
                await Store.FetchResellers();
            }
            catch (Exception)
            {
                MessageBox.Show("Нет подключение к серверу!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            FetchData();
        }
    }
}
