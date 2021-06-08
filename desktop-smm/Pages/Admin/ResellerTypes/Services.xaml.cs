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

namespace desktop_smm.Pages.Admin.ResellerTypes
{
    /// <summary>
    /// Логика взаимодействия для Services.xaml
    /// </summary>
    public partial class Services : Page
    {
        public Services()
        {
            InitializeComponent();
            cbReseller.ItemsSource = Store.resellers.Select(x => x.name);
        }

        void RenderServices()
        {
            lwTypesList.ItemsSource = null;
            if (cbReseller.SelectedItem != null)
                lwTypesList.ItemsSource = Store.resellerTypes.Where(x => x.reseller.name == cbReseller.SelectedItem.ToString());
        }

        private void cbReseller_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbReseller.SelectedIndex != -1)
                RenderServices();
        }
        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                (sender as Button).IsEnabled = false;

                if(MessageBox.Show("Вы действительно хотите удалить услугу?", "Удаление", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    var selected = Store.resellerTypes.FirstOrDefault(x => x.id.ToString() == (sender as Button).Tag.ToString());

                    var request = new Request("type", selected.id.ToString());
                    var response = await request.DeleteAPIData<RootResellerType>();
                    if (response.status)
                    {
                        await Store.FetchTypes();
                        RenderServices();

                        MessageBox.Show("Удаление произошло успешно!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else MessageBox.Show("Произошла ошибка при удалении!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception) { MessageBox.Show("Произошла ошибка при удалении услуги!", "Error", MessageBoxButton.OK, MessageBoxImage.Error); }
            finally { (sender as Button).IsEnabled = true; }
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var selected = Store.resellerTypes.FirstOrDefault(x => x.id.ToString() == (sender as Button).Tag.ToString());
                this.NavigationService.Navigate(new Added(selected));
            }
            catch (Exception) { MessageBox.Show("Произошла ошибка при редактировании услуги!", "Error", MessageBoxButton.OK, MessageBoxImage.Error); }
        }

    }
}
