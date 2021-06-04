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
using System.ComponentModel;

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

            var a = Services.Validator.Check(new Dictionary<Control, string>
            {
                { tb1, "Email" },
                { tb2, "Text" }
            });
            if (a is bool)
                MessageBox.Show("Ok");
            else
            {
                for (int i = 0; i < (a as List<String>).Count; i++)
                    MessageBox.Show((a as List<String>)[i]);
            }

            List<Page> pages = new List<Page> { new InfoPage(), new Main() };

            foreach (var item in stButtons.Children)
            {
                var button = item as Button;

                button.Click += (s, e) =>
                {
                    foreach (var j in stButtons.Children)
                        (j as Button).Background = (Brush)Application.Current.Resources["ColorButtonAccent"];

                    button.Background = (Brush)Application.Current.Resources["ColorButtonActive"];
                    MainContentLoad.NavigationService.Navigate(pages[int.Parse(button.Uid)]);
                };
            }

            MainContentLoad.NavigationService.Navigate(pages[0]);
        }
    }
}
