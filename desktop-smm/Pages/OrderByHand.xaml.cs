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

namespace desktop_smm.Pages
{
    /// <summary>
    /// Логика взаимодействия для OrderByHand.xaml
    /// </summary>
    public partial class OrderByHand : Page
    {
        public OrderByHand()
        {
            InitializeComponent();
            cbPayment.ItemsSource = Helper.SetPaymentsForCombobox();
            cbSocialNetwork.ItemsSource = Helper.SetSocialNetworksForCombobox();
        }

        private async void btnOrder_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                btnOrder.IsEnabled = false;

                var valid = Validator.Check(new Dictionary<Control, string>
                {
                    {tbSmmcraftId, "Number"},
                    {tbCost, "Number"},
                    {tbSpend, "Number"},
                    {cbPayment, "ComboBox"},
                    {cbSocialNetwork, "ComboBox"}
                });

                var validName = valid.GetType().Name;
                if (validName.StartsWith("List")) Validator.ShowErrors(valid as List<string>);
                else
                {
                    var request = new Request("order", "", new Dictionary<string, string>
                    {
                        {"idSmmcraft", tbSmmcraftId.Text},
                        {"socialNetwork", cbSocialNetwork.SelectedItem.ToString()},
                        {"cost", tbCost.Text},
                        {"spend", tbSpend.Text},
                        {"countViews", tbCountViews.Text},
                        {"payment", cbPayment.SelectedItem.ToString()},
                        {"userId", Store.user.id.ToString()}
                    });

                    var response = await request.PostAPIData<RootOrder>();
                    if (response.status)
                    {
                        MessageBox.Show("Заказ успешно добавлен!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                        Helper.ClearFields(new Control[]
                        {
                            tbSmmcraftId,cbPayment, cbSocialNetwork,
                            cbSocialNetwork, tbSpend
                        });
                    }
                    else MessageBox.Show("Произошла ошибка при добавлении заказа!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception){ MessageBox.Show("Произошла ошибка!\nВозможно, вы не заполнили поля ввода!", "Error", MessageBoxButton.OK, MessageBoxImage.Error); }
            finally { btnOrder.IsEnabled = true; }
        }
    }
}
