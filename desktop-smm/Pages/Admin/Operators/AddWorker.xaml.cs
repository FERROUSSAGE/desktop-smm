using desktop_smm.Models;
using desktop_smm.Services;
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

namespace desktop_smm.Pages.Admin.Operators
{
    /// <summary>
    /// Логика взаимодействия для AddWorker.xaml
    /// </summary>
    public partial class AddWorker : Page
    {
        public AddWorker()
        {
            InitializeComponent();
            cbRoles.ItemsSource = new List<string> { "Оператор", "Администратор" };
        }

        private async void btnAddWorker_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                btnAddWorker.IsEnabled = false;

                var valid = Validator.Check(new Dictionary<Control, string>
                {
                    {tbName, "Text"}, {cbRoles, "Text"}, 
                    {tbLogin, "LoginAndPassword"},{tbPassword, "LoginAndPassword"}
                });
                var validName = valid.GetType().Name;
                if (validName.StartsWith("List")) Validator.ShowErrors(valid as List<string>);
                else
                {
                    int roleId = cbRoles.SelectedItem.ToString() == "Оператор" ? 1 : 2;

                    var request = new Request("user", "registration", new Dictionary<string, string>
                    {
                        {"name", tbName.Text}, {"login", tbLogin.Text},
                        {"password", tbPassword.Text }, {"roleId", roleId.ToString()}
                    });

                    var response = await request.PostAPIData<RootUser>();
                    if (response.status)
                    {
                        await Store.FetchUsers();
                        MessageBox.Show($"Пользователь с именем - {tbName.Text} успешно зарегистрирован!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                        Helper.ClearFields(new Control[]
                        {
                            tbLogin, tbName, tbPassword, cbRoles
                        });
                    }
                    else MessageBox.Show(response.err.message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception){ MessageBox.Show("Произошла ошибка при регистрации пользователя", "Error", MessageBoxButton.OK, MessageBoxImage.Error); }
            finally { btnAddWorker.IsEnabled = true; }
        }
    }
}
