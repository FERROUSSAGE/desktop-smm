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
    /// Логика взаимодействия для Workers.xaml
    /// </summary>
    public partial class Workers : Page
    {
        public Workers()
        {
            InitializeComponent();
            RenderWorkers();
        }

        void RenderWorkers()
        {
            lwWorkerList.ItemsSource = null;
            lwWorkerList.ItemsSource = Store.users;
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                (sender as Button).IsEnabled = false;

                if (MessageBox.Show("Вы действительно хотите удалить оператора?", "Удаление", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    var selected = Store.users.FirstOrDefault(x => x.id.ToString() == (sender as Button).Tag.ToString());

                    var request = new Request("user", selected.id.ToString());
                    var response = await request.DeleteAPIData<RootUser>();
                    if (response.status)
                    {
                        await Store.FetchUsers();
                        RenderWorkers();

                        MessageBox.Show("Удаление произошло успешно!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else MessageBox.Show("Произошла ошибка при удалении!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception) { MessageBox.Show("Произошла ошибка при удалении оператора!", "Error", MessageBoxButton.OK, MessageBoxImage.Error); }
            finally { (sender as Button).IsEnabled = true; }
        }
    }
}
