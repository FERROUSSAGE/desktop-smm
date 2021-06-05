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
    /// Логика взаимодействия для Main.xaml
    /// </summary>
    public partial class Main : Page
    {
        public Main()
        {
            InitializeComponent();
            List<Page> resellerPages = new List<Page> { new Adcore(), new Smmok(), new Vktarget(), new Spanel() };

            foreach (var item in GridButtons.Children)
            {
                var button = (item as Border).Child as TextBlock;

                button.MouseDown += (s, e) =>
                {
                    foreach (var j in GridButtons.Children)
                        ((j as Border).Child as TextBlock).Foreground = (Brush)Application.Current.Resources["ColorButtonAccent"];

                    button.Foreground = (Brush)Application.Current.Resources["ColorButtonActive"];
                    ResellerLoad.NavigationService.Navigate(resellerPages[int.Parse(button.Uid)]);
                };
            }
        }
    }
}
