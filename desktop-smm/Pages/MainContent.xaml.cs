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
using desktop_smm.Pages.Resellers;
using desktop_smm.Pages;
using desktop_smm.Windows;
using System.ComponentModel;
using System.Diagnostics;
using desktop_smm.Services;

namespace desktop_smm.Pages
{
    /// <summary>
    /// Логика взаимодействия для MainContent.xaml
    /// </summary>
    public partial class MainContent : Page
    {
        
        public MainContent()
        {
            InitializeComponent();

            List<Page> pages = new List<Page> { new InfoPage(), new Main(), new OrderByHand(), new Mail() };

            foreach (var item in stButtons.Children)
            {
                var button = item as Button;

                if (Store.user.roleId == 2) btnLoadAdmin.Visibility = Visibility.Visible;

                button.Click += (s, e) =>
                {
                    foreach (var j in stButtons.Children)
                        (j as Button).Background = (Brush)Application.Current.Resources["ColorButtonAccent"];

                    button.Background = (Brush)Application.Current.Resources["ColorButtonActive"];
                    if (button.Tag != null && button.Tag.ToString() == "orders") new OrderListModal().Show();
                    else
                    {
                        if (button.Uid == "4") this.NavigationService.Navigate(new Admin.Main());
                        else MainContentLoad.NavigationService.Navigate(pages[int.Parse(button.Uid)]);
                    }
                };
            }

            foreach(var item in developerButtons.Children) ((TextBlock)item).MouseDown += (s, e) => Process.Start(((TextBlock)item).Tag.ToString());

            MainContentLoad.NavigationService.Navigate(pages[0]);
        }
    }
}
