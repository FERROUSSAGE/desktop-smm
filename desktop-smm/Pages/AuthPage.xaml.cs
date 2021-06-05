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
using System.Windows.Navigation;
using System.Windows.Shapes;
using desktop_smm.Services;

namespace desktop_smm.Pages
{
    /// <summary>
    /// Логика взаимодействия для AuthPage.xaml
    /// </summary>
    public partial class AuthPage : Page
    {
        public AuthPage()
        {
            InitializeComponent();
        }

        async void FetchData()
        {
            await Store.FetchOrders();
            await Store.FetchTypes();
            await Store.FetchResellers();
        }

        private void btnAuth_Click(object sender, RoutedEventArgs e)
        {
            var valid = Validator.Check(new Dictionary<char[], string>
            {
                {tbPassword.Text.ToArray(), "LoginAndPassword"},
                {tbLogin.Text.ToArray(), "LoginAndPassword" }
            });

            var validName = valid.GetType().Name;
            if (validName.StartsWith("List")) Validator.ShowErrors(valid as List<string>);
            else
            {
                this.NavigationService.Navigate(new MainContent());
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            FetchData();
        }
    }
}
