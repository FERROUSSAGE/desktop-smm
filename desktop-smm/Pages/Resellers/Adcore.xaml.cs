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
                    if(type.name == cbSocialNetwork.SelectedItem.ToString())
                        cbResellerType.Items.Add(type.name + " | " + type.description + " | " + type.type);
            };
     
            cbPayment.ItemsSource = Helper.SetPaymentsForCombobox();
        }

        //bool Validate(Dictionary<Control, string> array)
        //{
        //    //var response = Validator.Check(array);
        //    //if(!(response is bool))
        //    //    for (int i = 0; i < (response as List<String>).Count; i++)
        //    //    {
        //    //        MessageBox.Show((response as List<String>)[i]);
        //    //        return false;
        //    //    }
        //    //else if((response is bool) == false)
        //    //{
        //    //    return false;
        //    //}
        //    //return true;
        //}

        private void btnOrder_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //if (Validate(new Dictionary<Control, string>
                //{
                //    {tbCost, "Numbers"}, {tbCountOrdered, "Numbers"}, {tbLink, "Text"},
                //    {tbSmmcraftId, "Numbers"}, {cbPayment, "Text"}, {cbResellerType, "Numbers"}, {cbSocialNetwork, "Text"}
                //}))
                //{
                //    //if (Store.orders != null)
                //    //{
                //    //    var type = Store.resellerTypes.FirstOrDefault(x => x.type == cbResellerType.Tag.ToString());

                //    //var requestDictionary = new Dictionary<string, string>();
                //    //requestDictionary.Add("resellerType", cbResellerType.Tag.ToString());
                //    //requestDictionary.Add("link", tbLink.Text);
                //    //requestDictionary.Add("countOrdered", tbCountOrdered.Text);
                //    //requestDictionary.Add("type", type?.price.ToString());

                //    //    //var request = new Request(requestDictionary, "adcore", "create");
                //    //    MessageBox.Show("Клик");
                //    //}
                //    //else btnOrder_Click(null, null);
                //}
            }
            catch (Exception) {}
        }
    }
}
