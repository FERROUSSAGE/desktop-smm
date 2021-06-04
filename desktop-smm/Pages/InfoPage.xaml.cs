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

namespace desktop_smm.Pages
{
    /// <summary>
    /// Логика взаимодействия для InfoPage.xaml
    /// </summary>
    public partial class InfoPage : Page
    {
        public InfoPage()
        {
            InitializeComponent();

            foreach(var item in stButtons.Children)
            {
                var button = item as Button;
                var content = button.Content as TextBlock;

                button.Click += (s, e) =>
                {
                    foreach(var j in stButtons.Children) 
                        ((j as Button).Content as TextBlock).Foreground = (Brush)Application.Current.Resources["ColorTextAccent"];

                    content.Foreground = (Brush)Application.Current.Resources["ColorButtonActive"];
                    tbTitle.Text = content.Text;
                    tbDescription.Text = content.Tag.ToString();
                };
            }
        }
    }
}
