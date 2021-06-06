using desktop_smm.Models;
using desktop_smm.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace desktop_smm.Windows
{
    /// <summary>
    /// Логика взаимодействия для OrderListModal.xaml
    /// </summary>
    public partial class OrderListModal : Window
    {
        private readonly Debouncer _searchDebouncer;
        private int page = 1;
        public OrderListModal()
        {
            InitializeComponent();

            _searchDebouncer = new Debouncer(TimeSpan.FromSeconds(.75), Search);

            RenderOrders();
            tbCountPage.Text = $"Страница: {page}";

            tbAllOrders.Text = $"Всего: {Store.countPage}";

            try
            {
                foreach (var item in gridButtons.Children)
                {
                    var button = item as TextBlock;

                    button.MouseDown += async (s, e) =>
                    {
                        switch (button.Uid)
                        {
                            case "0":
                                page = 1;
                                break;
                            case "1":
                                if (page >= 2) page -= 1;
                                break;
                            case "2":
                                if (page <= Store.lastPage) page += 1;
                                break;
                            case "3":
                                page = Store.lastPage;
                                break;
                        }

                        await Store.FetchOrders(page.ToString());
                        tbCountPage.Text = $"Страница: {page}";
                        RenderOrders();
                    };
                }
            }
            catch (Exception){ MessageBox.Show("Произошла ошибка при получении заказов с предыдущей/следующей страницы!", "Error", MessageBoxButton.OK, MessageBoxImage.Error); }
        }

        void RenderOrders()
        {
            lwOrderList.ItemsSource = null;
            lwOrderList.ItemsSource = Store.orders;
        }

        void Search()
        {
            Dispatcher.BeginInvoke(new ThreadStart(async delegate
            {
                try
                {
                    if (!string.IsNullOrEmpty(tbSearchOrder.Text))
                    {
                        var request = new Request("orderByText", "text", new Dictionary<string, string>
                    {
                        {"text", tbSearchOrder.Text }
                    });
                        var result = await request.PostAPIData<RootOrder>();
                        Store.orders = result?.response?.orders;
                        RenderOrders();
                    }
                    else { await Store.FetchOrders("1"); RenderOrders(); }
                }
                catch (Exception){ MessageBox.Show("Произошла ошибка при попытке найти заказ!", "Error", MessageBoxButton.OK, MessageBoxImage.Error); }
            }));
            
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            _searchDebouncer.Invoke();
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            await Store.FetchOrders("1");
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if(MessageBox.Show("Вы действительно хотите удалить заказ?", "Question", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    int selected = Store.orders.FirstOrDefault(x => x.id == int.Parse((sender as Button).Tag.ToString())).id;

                    var request = new Request("order", selected.ToString());
                    var response = await request.DeleteAPIData<RootOrder>();
                    if (response.status)
                    {
                        await Store.FetchOrders(page.ToString());
                        tbCountPage.Text = $"Страница: {page}";
                        RenderOrders();
                        MessageBox.Show("Заказ успешно удален!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else MessageBox.Show("Произошла ошибка при удалении!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception){ MessageBox.Show("Произошла ошибка при удалении!", "Error", MessageBoxButton.OK, MessageBoxImage.Error); }
        }
    }
}
