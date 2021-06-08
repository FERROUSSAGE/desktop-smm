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
using desktop_smm.Models;
using desktop_smm.Models.Resellers;
using desktop_smm.Services;

namespace desktop_smm.Pages.Resellers
{
    /// <summary>
    /// Логика взаимодействия для Adcore.xaml
    /// </summary>
    public partial class Adcore : Page
    {
        private List<ResellerType> types;
        public Adcore()
        {
            InitializeComponent();
            types = Store.resellerTypes.Where(x => x.resellerId == 1).ToList();

            cbSocialNetwork.ItemsSource = Helper.SetItemsForCombobox(types).Select(x => x.name).Distinct();
            cbSocialNetwork.SelectionChanged += (s, e) =>
            {
                cbResellerType.Items.Clear();
                    foreach (var type in Helper.SetItemsForCombobox(types))
                        if (cbSocialNetwork.SelectedItem != null && type.name == cbSocialNetwork.SelectedItem.ToString())
                            cbResellerType.Items.Add($"{type.name} | {type.description} | {type.type}");
            };
     
            cbPayment.ItemsSource = Helper.SetPaymentsForCombobox();

            this.Loaded += async (s, e) => tbBalance.Text = await Helper.GetBalance("adcore") + "р";
        }

        private async void btnOrder_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                btnOrder.IsEnabled = true;
                
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
                        var requestAdcore = new Request("adcore", "create", new Dictionary<string, string>
                        {
                            {"link", tbLink.Text},
                            {"countOrdered", tbCountOrdered.Text},
                            {"price", type.price.ToString()},
                            {"type", type.type}
                        });
                        var responseAdcore = await requestAdcore.PostAPIData<RootAdcore>();
                        if (responseAdcore.status)
                        {
                            if (checkDontSave.IsChecked == false)
                            {
                                var requestOrder = new Request("order", "", new Dictionary<string, string>
                                {
                                    {"idSmmcraft", tbSmmcraftId.Text},
                                    {"idProject", responseAdcore.response.idProject.ToString()},
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
                                    tbBalance.Text = await Helper.GetBalance("adcore") + "р";
                                    Helper.ClearFields(new Control[]
                                    {
                                        tbSmmcraftId, tbLink, tbCountOrdered, tbCost,
                                        cbPayment, cbResellerType, cbSocialNetwork
                                    });
                                    checkDontSave.IsChecked = false;
                                }
                                else MessageBox.Show("Произошла ошибка при создании заказа!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                            }
                            else
                            {
                                MessageBox.Show("Заказ без сохранения успешно создан!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                                tbBalance.Text = await Helper.GetBalance("adcore") + "р";
                                Helper.ClearFields(new Control[]
                                {
                                        tbLink, tbCountOrdered, cbResellerType, cbSocialNetwork
                                });
                            }
                        }
                        else MessageBox.Show(responseAdcore.response.msg, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    else MessageBox.Show("Заказ с таким -ID- существует!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception) { MessageBox.Show("Произошла ошибка!\nВозможно, поля ввода не заполнены!", "Error", MessageBoxButton.OK, MessageBoxImage.Error); }
            finally { btnOrder.IsEnabled = true; }
        }
    }
}
