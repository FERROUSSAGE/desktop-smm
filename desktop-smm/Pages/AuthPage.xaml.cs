using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using desktop_smm.Models;
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

        private async void btnAuth_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                btnAuth.IsEnabled = false;

                var valid = Validator.Check(new Dictionary<Control, string>
                {
                    {tbLogin, "LoginAndPassword"},
                    {tbPassword, "LoginAndPassword"},
                });

                var validName = valid.GetType().Name;
                if (validName.StartsWith("List")) Validator.ShowErrors(valid as List<string>);
                else
                {
                    var request = new Request("user", "login", new Dictionary<string, string>
                    {
                        {"login", tbLogin.Text},
                        {"password", tbPassword.Text}
                    });
                    var response = await request.PostAPIData<RootUser>();

                    if (response.status)
                    {
                        Store.user = response.users.FirstOrDefault();
                        this.NavigationService.Navigate(new MainContent());
                    }
                    else MessageBox.Show(response.err.message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);

                }
            }
            catch (Exception) { MessageBox.Show("Нет подключение к серверу!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error); }
            finally { btnAuth.IsEnabled = true; }
        }
    }
}
