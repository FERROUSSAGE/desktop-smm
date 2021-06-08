using desktop_smm.Pages.Admin.Operators;
using desktop_smm.Pages.Admin.ResellerTypes;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace desktop_smm.Pages.Admin
{
    /// <summary>
    /// Логика взаимодействия для Main.xaml
    /// </summary>
    public partial class Main : Page
    {
        public Main()
        {
            InitializeComponent();
            List<Page> pages = new List<Page> { new Resellers(), new MainIndex(), new IndexMain() };

            foreach (var item in stButtons.Children)
            {
                var button = item as Button;

                button.Click += (s, e) =>
                {
                    foreach (var j in stButtons.Children)
                        (j as Button).Background = (Brush)Application.Current.Resources["ColorButtonAccent"];

                    button.Background = (Brush)Application.Current.Resources["ColorButtonActive"];
                    if (button.Uid == "3" && this.NavigationService.CanGoBack)
                        this.NavigationService.Navigate(new MainContent());
                    else AdminContentLoad.NavigationService.Navigate(pages[int.Parse(button.Uid)]);
                };
            }

            foreach (var item in developerButtons.Children) ((TextBlock)item).MouseDown += (s, e) => Process.Start(((TextBlock)item).Tag.ToString());
        }
    }
}
