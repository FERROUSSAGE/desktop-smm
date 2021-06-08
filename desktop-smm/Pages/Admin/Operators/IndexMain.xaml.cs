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

namespace desktop_smm.Pages.Admin.Operators
{
    /// <summary>
    /// Логика взаимодействия для IndexMain.xaml
    /// </summary>
    public partial class IndexMain : Page
    {
        public IndexMain()
        {
            InitializeComponent();
            foreach (var item in GridButtons.Children)
            {
                var button = (item as Border).Child as TextBlock;

                button.MouseDown += (s, e) =>
                {
                    foreach (var j in GridButtons.Children)
                        ((j as Border).Child as TextBlock).Foreground = (Brush)Application.Current.Resources["ColorButtonAccent"];

                    button.Foreground = (Brush)Application.Current.Resources["ColorButtonActive"];
                    if (button.Uid == "0")
                        OperatorsLoad.NavigationService.Navigate(new Workers());
                    else OperatorsLoad.NavigationService.Navigate(new AddWorker());
                };
            }
        }
    }
}
