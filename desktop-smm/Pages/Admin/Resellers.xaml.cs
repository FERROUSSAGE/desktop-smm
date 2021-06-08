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

namespace desktop_smm.Pages.Admin
{
    /// <summary>
    /// Логика взаимодействия для Resellers.xaml
    /// </summary>
    public partial class Resellers : Page
    {
        public Resellers()
        {
            InitializeComponent();
        }

        private void btnEdit_MouseDown(object sender, MouseButtonEventArgs e)
        {
            cbReseller.ItemsSource = Store.resellers.Select(x => x.name);
            stContent.Visibility = Visibility.Visible;
        }

        private async void btnChangeApiKey_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                btnChangeApiKey.IsEnabled = false;

                if (!String.IsNullOrEmpty(tbKey.Text.Trim()) && cbReseller.SelectedItem != null)
                {
                    var reseller = Store.resellers.FirstOrDefault(x => x.name == cbReseller.SelectedItem.ToString());
                    var request = new Request("reseller", reseller.id.ToString(), new Dictionary<string, string>
                        { {"name", reseller.name}, {"api_key", tbKey.Text} });
                    var response = await request.PutAPIData<RootReseller>();
                    if (response.status)
                    {
                        MessageBox.Show(response.message, "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                        Helper.ClearFields(new Control[] { tbKey, cbReseller });
                    }
                    else MessageBox.Show(response.message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else MessageBox.Show("Заполните поле ввода, прежде чем изменять ключ!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception) { MessageBox.Show("Произошла ошибка при изменении ключа!", "Erorr", MessageBoxButton.OK, MessageBoxImage.Error); }
            finally { btnChangeApiKey.IsEnabled = true; }
        }
    }
}
