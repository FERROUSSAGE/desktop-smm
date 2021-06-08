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
    /// Логика взаимодействия для Spanel.xaml
    /// </summary>
    public partial class Spanel : Page
    {
        private List<ResellerType> types;
        public Spanel()
        {
            InitializeComponent();
            types = Store.resellerTypes.Where(x => x.resellerId == 4).ToList();

            cbSocialNetwork.ItemsSource = Helper.SetItemsForCombobox(types).Select(x => x.name).Distinct();
            cbSocialNetwork.SelectionChanged += (s, e) =>
            {
                cbResellerType.Items.Clear();
                foreach (var type in Helper.SetItemsForCombobox(types))
                    if (type.name == cbSocialNetwork.SelectedItem.ToString())
                        cbResellerType.Items.Add($"{type.name} | {type.description} | {type.type}");
            };

            cbPayment.ItemsSource = Helper.SetPaymentsForCombobox();

            this.Loaded += async (s, e) => tbBalance.Text = await Helper.GetBalance("spanel") + "р";
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
                    string[] selectedType = cbResellerType.SelectedItem.ToString().Split('|');

                    var type = Store.resellerTypes.FirstOrDefault(x => x.type == selectedType[3].Trim());

                    if (int.Parse(tbCountOrdered.Text) < int.Parse(selectedType[2].Split('-')[1].Trim()))
                    {
                        MessageBox.Show("Введеное количество исполнений ниже минимального количества услуги!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }

                    var checkOrder = new Request("checkOrder", $"check/{tbSmmcraftId.Text}");
                    var responseCheck = await checkOrder.GetApiData<RootOrder>();
                    if (responseCheck.status)
                    {
                        var requestSpanel = new Request("spanel", "create", new Dictionary<string, string>
                        {
                            {"link", tbLink.Text},
                            {"countOrdered", tbCountOrdered.Text},
                            {"type", type.type}
                        });
                        var responseSpanel = await requestSpanel.PostAPIData<RootSpanel>();
                        if (responseSpanel.status)
                        {
                            if(checkDontSave.IsChecked == false)
                            {
                                var requestOrder = new Request("order", "", new Dictionary<string, string>
                                {
                                    {"idSmmcraft", tbSmmcraftId.Text},
                                    {"idProject", responseSpanel.response.idProject.ToString()},
                                    {"socialNetwork", cbSocialNetwork.SelectedItem.ToString()},
                                    {"link", tbLink.Text},
                                    {"cost", tbCost.Text},
                                    {"spend", responseSpanel.response.spend.ToString()},
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
                                    tbBalance.Text = await Helper.GetBalance("spanel") + "р";
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
                                tbBalance.Text = await Helper.GetBalance("spanel") + "р";
                                Helper.ClearFields(new Control[]
                                {
                                        tbLink, tbCountOrdered, cbResellerType, cbSocialNetwork
                                });
                            }
                        }
                        else MessageBox.Show(responseSpanel.response.error, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    else MessageBox.Show("Заказ с таким -ID- существует!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception) { MessageBox.Show("Произошла ошибка!", "Error", MessageBoxButton.OK, MessageBoxImage.Error); }
            finally { btnOrder.IsEnabled = true; }
        }
    }
}
