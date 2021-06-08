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
    /// Логика взаимодействия для Main.xaml
    /// </summary>
    public partial class MainIndex : Page
    {
        public MainIndex()
        {
            InitializeComponent();
            List<Page> resellerTypesPages = new List<Page> { new Services(), new Added(null) };

            foreach (var item in GridButtons.Children)
            {
                var button = (item as Border).Child as TextBlock;

                button.MouseDown += (s, e) =>
                {
                    foreach (var j in GridButtons.Children)
                        ((j as Border).Child as TextBlock).Foreground = (Brush)Application.Current.Resources["ColorButtonAccent"];

                    button.Foreground = (Brush)Application.Current.Resources["ColorButtonActive"];
                    ResellerTypesLoad.NavigationService.Navigate(resellerTypesPages[int.Parse(button.Uid)]);
                };
            }
        }
    }
}
