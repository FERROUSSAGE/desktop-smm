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
    /// Логика взаимодействия для Added.xaml
    /// </summary>
    public partial class Added : Page
    {
        private int typeId;
        public Added(ResellerType type)
        {
            InitializeComponent();
            cbReseller.ItemsSource = Store.resellers.Select(x => x.name);
            cbSocialNetwork.ItemsSource = Helper.SetSocialNetworksForCombobox();
            if (type != null)
                PushFields(type);
        }

        void PushFields(ResellerType type)
        {
            cbSocialNetwork.Text = type.name;
            tbResellerType.Text = type.type;
            tbPrice.Text = type.price.ToString();
            tbDescription.Text = type.description;
            cbReseller.Text = type.reseller.name;
            btnAddType.Content = "Изменить";

            typeId = type.id;
        }

        private async void btnAddType_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                btnAddType.IsEnabled = false;

                var valid = Validator.Check(new Dictionary<Control, string>
                {
                    {cbSocialNetwork, "Text" }, {cbReseller, "Text" }, {tbPrice, "Number"}
                });
                var validName = valid.GetType().Name;
                if (validName.StartsWith("List")) Validator.ShowErrors(valid as List<string>);
                else if (String.IsNullOrEmpty(tbDescription.Text.Trim()))
                {
                    Validator.SelectError(tbDescription);
                    MessageBox.Show($"Была передана пустая строка\n{tbDescription.Uid}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    Validator.ResetSelect(tbDescription);
                }
                else if(String.IsNullOrEmpty(tbResellerType.Text))
                {
                    Validator.SelectError(tbResellerType);
                    MessageBox.Show($"Была передана пустая строка\n{tbResellerType.Uid}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    Validator.ResetSelect(tbResellerType);

                    if(btnAddType.Content.ToString() == "Добавить")
                    {
                        var reseller = Store.resellers.FirstOrDefault(x => x.name == cbReseller.SelectedItem.ToString());

                        var request = new Request("type", "", new Dictionary<string, string>
                        {
                            {"name", cbSocialNetwork.SelectedItem.ToString()}, {"price", tbPrice.Text}, {"resellerId", reseller.id.ToString()},
                            {"description", tbDescription.Text.Trim()}, {"type", tbResellerType.Text}
                        });
                        var response = await request.PostAPIData<RootResellerType>();
                        if (response.status)
                        {
                            await Store.FetchTypes();
                            MessageBox.Show("Добавление произошло успешно!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                            Helper.ClearFields(new Control[]
                            {
                                tbDescription, cbSocialNetwork, tbPrice, tbResellerType, cbReseller
                            });
                        }
                        else MessageBox.Show("Произошла ошибка при добавлении услуги!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    else
                    {
                        var reseller = Store.resellers.FirstOrDefault(x => x.name == cbReseller.SelectedItem.ToString());

                        var request = new Request("type", typeId.ToString(), new Dictionary<string, string>
                        {
                            {"name", cbSocialNetwork.SelectedItem.ToString()}, {"price", tbPrice.Text}, {"resellerId", reseller.id.ToString()},
                            {"description", tbDescription.Text.Trim()}, {"type", tbResellerType.Text}
                        });
                        var response = await request.PutAPIData<RootResellerType>();
                        if (response.status)
                        {
                            await Store.FetchTypes();
                            MessageBox.Show("Редактирование произошло успешно!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                            Helper.ClearFields(new Control[]
                            {
                                tbDescription, cbSocialNetwork, tbPrice, tbResellerType, cbReseller
                            });
                        }
                        else MessageBox.Show("Произошла ошибка при редактировании услуги!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            catch (Exception) { MessageBox.Show("Произошла ошибка при добавлении/редактировании услуги!", "Error", MessageBoxButton.OK, MessageBoxImage.Error); }
            finally { btnAddType.IsEnabled = true; }
        }
    }
}
