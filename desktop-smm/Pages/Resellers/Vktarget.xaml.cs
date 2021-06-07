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
    /// Логика взаимодействия для Vktarget.xaml
    /// </summary>
    public partial class Vktarget : Page
    {
        private List<ResellerType> types;
        public Vktarget()
        {
            InitializeComponent();
            types = Store.resellerTypes.Where(x => x.resellerId == 3).ToList();

            cbSocialNetwork.ItemsSource = Helper.SetItemsForCombobox(types).Select(x => x.name).Distinct();
            cbSocialNetwork.SelectionChanged += (s, e) =>
            {
                cbResellerType.Items.Clear();
                foreach (var type in Helper.SetItemsForCombobox(types))
                    if (type.name == cbSocialNetwork.SelectedItem.ToString())
                        cbResellerType.Items.Add($"{type.name} | {type.description} | {type.type}");
            };

            cbPayment.ItemsSource = Helper.SetPaymentsForCombobox();
        }

        private async void btnOrder_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                btnOrder.IsEnabled = false;

                object valid = null;

                if(checkDontSave.IsChecked == false)
                {
                    valid = Validator.Check(new Dictionary<Control, string>
                    {
                        {tbSmmcraftId, "Number"},
                        {tbLink, "Link"},
                        {tbCountOrdered, "Number"},
                        {tbCost, "Number"},
                        {cbPayment, "ComboBox"},
                        {cbSocialNetwork, "ComboBox"},
                        {cbResellerType, "ComboBox"}
                    });
                }
                else
                {
                    valid = Validator.Check(new Dictionary<Control, string>
                    {
                        {tbSmmcraftId, "Number"},
                        {tbLink, "Link"},
                        {tbCountOrdered, "Number"},
                        {cbSocialNetwork, "ComboBox"},
                        {cbResellerType, "ComboBox"}
                    });
                }


                var validName = valid.GetType().Name;
                if (validName.StartsWith("List")) Validator.ShowErrors(valid as List<string>);
                else
                {
                    var type = Store.resellerTypes.FirstOrDefault(x => x.type == cbResellerType.SelectedItem.ToString().Split('|')[2].Trim());

                    var checkOrder = new Request("checkOrder", $"check/{tbSmmcraftId.Text}");
                    var responseCheck = await checkOrder.GetApiData<RootOrder>();
                    if (responseCheck.status)
                    {
                        var requestVktarget = new Request("vktarget", "create", new Dictionary<string, string>
                        {
                            {"link", tbLink.Text},
                            {"countOrdered", tbCountOrdered.Text},
                            {"idSmmcraft", tbSmmcraftId.Text},
                            {"type", type.type}
                        });
                        var responseVktarget = await requestVktarget.PostAPIData<RootVktarget>();
                        if (responseVktarget.status)
                        {
                            if(checkDontSave.IsChecked == false)
                            {
                                var requestOrder = new Request("order", "", new Dictionary<string, string>
                                {
                                    {"idSmmcraft", tbSmmcraftId.Text},
                                    {"idProject", responseVktarget.response.id.ToString()},
                                    {"socialNetwork", cbSocialNetwork.SelectedItem.ToString()},
                                    {"link", tbLink.Text},
                                    {"cost", tbCost.Text},
                                    {"spend", Helper.Rounded((type.price * int.Parse(tbCountOrdered.Text)))},
                                    {"countOrdered", tbCountOrdered.Text},
                                    {"payment", cbPayment.SelectedItem.ToString()},
                                    {"resellerId", type.resellerId.ToString()},
                                    {"resellerTypeId", type.id.ToString()},
                                    {"userId", Store.user.id.ToString()}
                                });
                                var responseOrder = await requestOrder.PostAPIData<RootOrder>();
                                if (responseOrder.status)
                                {
                                    MessageBox.Show("Заказ успешно создан!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                                    Helper.ClearFields(new Control[]
                                    {
                                        tbSmmcraftId, tbLink, tbCountOrdered, tbCost,
                                        cbPayment, cbResellerType, cbSocialNetwork
                                    });
                                }
                                else MessageBox.Show("Произошла ошибка при создании заказа!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                            }
                            else
                            {
                                MessageBox.Show("Заказ без сохранения успешно создан!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                                Helper.ClearFields(new Control[]
                                {
                                        tbLink, tbCountOrdered, cbResellerType, cbSocialNetwork, tbSmmcraftId
                                });
                            }
                        }
                        else MessageBox.Show(responseVktarget.response.msg, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    else MessageBox.Show("Заказ с таким -ID- существует!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception) { MessageBox.Show("Произошла ошибка!", "Error", MessageBoxButton.OK, MessageBoxImage.Error); }
            finally { btnOrder.IsEnabled = true; }
        }
    }
}
