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
    /// Логика взаимодействия для Smmok.xaml
    /// </summary>
    public partial class Smmok : Page
    {
        private List<ResellerType> types;
        public Smmok()
        {
            InitializeComponent();
            types = Store.resellerTypes.Where(x => x.resellerId == 2).ToList();

            foreach(var type in types) cbResellerType.Items.Add($"{type.name} | {type.description} | {type.type}");
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
                        {cbResellerType, "ComboBox"},
                    });
                }
                else
                {
                    valid = Validator.Check(new Dictionary<Control, string>
                    {
                        {tbSmmcraftId, "Number"},
                        {tbLink, "Link"},
                        {tbCountOrdered, "Number"},
                        {cbResellerType, "ComboBox"},
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
                        var requestSmmok = new Request("smmok", "create", new Dictionary<string, string>
                        {
                            {"link", tbLink.Text},
                            {"countOrdered", tbCountOrdered.Text},
                            {"idSmmcraft", tbSmmcraftId.Text},
                            {"type", type.type}
                        });
                        var responseSmmok = await requestSmmok.PostAPIData<RootSmmok>();
                        if (responseSmmok.status)
                        {
                            if(checkDontSave.IsChecked == false)
                            {
                                var requestOrder = new Request("order", "", new Dictionary<string, string>
                                {
                                    {"idSmmcraft", tbSmmcraftId.Text},
                                    {"idProject", responseSmmok.response.id.ToString()},
                                    {"socialNetwork", "Facebook"},
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
                                        cbPayment, cbResellerType
                                    });
                                }
                                else MessageBox.Show("Произошла ошибка при создании заказа!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                            }
                            else
                            {
                                MessageBox.Show("Заказ без сохранения успешно создан!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                                Helper.ClearFields(new Control[]
                                {
                                        tbLink, tbCountOrdered, cbResellerType, tbSmmcraftId
                                });
                            }
                        }
                        else MessageBox.Show(responseSmmok.response.msg, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    else MessageBox.Show("Заказ с таким -ID- существует!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception) { MessageBox.Show("Произошла ошибка!", "Error", MessageBoxButton.OK, MessageBoxImage.Error); }
            finally { btnOrder.IsEnabled = true; }
        }
    }
}
