using desktop_smm.Models;
using desktop_smm.Models.Resellers;
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

namespace desktop_smm.Pages.Resellers
{
    /// <summary>
    /// Логика взаимодействия для StreamBooster.xaml
    /// </summary>
    public partial class StreamBooster : Page
    {
        public StreamBooster()
        {
            InitializeComponent();
            cbPayment.ItemsSource = Helper.SetPaymentsForCombobox();
        }

        private async void btnOrder_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                btnOrder.IsEnabled = false;
                object valid = null;

                if(checkDontSave.IsChecked == true)
                {
                    valid = Validator.Check(new Dictionary<Control, string>
                    {
                        {cbSocialNetwork, "Text"},
                        {tbUUID, "Uid"},
                        {tbThreadsCount, "Number"},
                        {tbTimer, "Number"}
                    });
                }
                else
                {
                    valid = Validator.Check(new Dictionary<Control, string>
                    {
                        {tbSmmcraftId, "Number"},
                        {cbSocialNetwork, "Text"},
                        {tbUUID, "Uid"},
                        {tbThreadsCount, "Number"},
                        {tbTimer, "Number"},
                        {tbViews, "Number"},
                        {cbPayment, "Text"},
                        {tbCost, "Number"}
                    });
                }

                var validName = valid.GetType().Name;
                if (validName.StartsWith("List")) Validator.ShowErrors(valid as List<string>);
                else
                {
                    string link = $"https://www.youtube.com/watch?v={tbUUID.Text}";
                    var resellerType = Store.resellerTypes.Find(x => x.name == "YouTube" && x.resellerId == 1 && x.description.ToLower().Contains("лайки"));

                    if(checkDontSave.IsChecked == true)
                    {
                        var request = new Request("streambooster", "start", new Dictionary<string, string>
                        {
                            {"uuid", tbUUID.Text}, {"threads", tbThreadsCount.Text}, {"timer", tbTimer.Text},
                            {"service", (cbSocialNetwork.SelectedItem as ComboBoxItem).Tag.ToString()}
                        });
                        var response = await request.PostAPIData<RootStreambooster>();
                        if (response.status)
                        {
                            MessageBox.Show($"Запуск произошел успешно!\nUUID - {tbUUID.Text}", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

                            if (!String.IsNullOrEmpty(tbCountLikes.Text))
                            {
                                var requestLikes = new Request("adcore", "create", new Dictionary<string, string> 
                                {
                                    {"type", resellerType.type}, {"link", link}, 
                                    {"countOrdered", tbCountLikes.Text}, {"price", resellerType.price.ToString()}

                                });
                                var responseAdcore = await requestLikes.PostAPIData<RootAdcore>();
                                if (!responseAdcore.status)
                                    MessageBox.Show("Произошла ошибка при включении лайков!\nПопробуйте включить лайки через соответствующий реселлер!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                            }

                            Helper.ClearFields(new Control[]
                            {
                                cbSocialNetwork, tbThreadsCount, tbUUID, tbTimer, tbCountLikes
                            });
                        }
                        else MessageBox.Show("Произошла ошибка при запуске стрима!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    else 
                    {
                        var request = new Request("streambooster", "start", new Dictionary<string, string>
                        {
                            {"uuid", tbUUID.Text}, {"threads", tbThreadsCount.Text}, {"timer", tbTimer.Text},
                            {"service", (cbSocialNetwork.SelectedItem as ComboBoxItem).Tag.ToString()}
                        });
                        var response = await request.PostAPIData<RootStreambooster>();
                        if (response.status)
                        {
                            if (!String.IsNullOrEmpty(tbCountLikes.Text))
                            {
                                var requestLikes = new Request("adcore", "create", new Dictionary<string, string>
                                {
                                    {"type", resellerType.type}, {"link", link},
                                    {"countOrdered", tbCountLikes.Text}, {"price", resellerType.price.ToString()}

                                });
                                var responseAdcore = await requestLikes.PostAPIData<RootAdcore>();
                                if (!responseAdcore.status)
                                    MessageBox.Show("Произошла ошибка при включении лайков!\nПопробуйте включить лайки через соответствующий реселлер!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                            }

                            var requestOrder = new Request("order", "", new Dictionary<string, string>
                            {
                                {"idSmmcraft", tbSmmcraftId.Text},
                                {"socialNetwork", (cbSocialNetwork.SelectedItem as ComboBoxItem).Content.ToString()},
                                {"link", link},
                                {"cost", tbCost.Text},
                                {"spend", !string.IsNullOrEmpty(tbCountLikes.Text) ? Helper.Rounded(resellerType.price * int.Parse(tbCountLikes.Text)) : "0"},
                                {"countViews", tbViews.Text},
                                {"payment", cbPayment.SelectedItem.ToString()},
                                {"userId", Store.user.id.ToString()}
                            });    
                            var responseOrder = await requestOrder.PostAPIData<RootOrder>();
                            if (responseOrder.status)
                            {
                                MessageBox.Show($"Запуск произошел успешно!\nUUID - {tbUUID.Text}\nЗаписан в список заказов!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                                Helper.ClearFields(new Control[]
                                {
                                    tbSmmcraftId, cbSocialNetwork, cbPayment, tbCost, tbCountLikes,
                                    tbThreadsCount, tbTimer, tbUUID, tbViews
                                });

                                checkDontSave.IsChecked = false;
                            }
                        }
                    }
                }
            }
            catch (Exception) { MessageBox.Show("Произошла ошибка при запуске стрима!", "Error", MessageBoxButton.OK, MessageBoxImage.Error); }
            finally { btnOrder.IsEnabled = true; }
        }
    }
}
