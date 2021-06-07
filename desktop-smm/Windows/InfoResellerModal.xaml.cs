using desktop_smm.Models.Resellers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace desktop_smm.Windows
{
    /// <summary>
    /// Логика взаимодействия для InfoResellerModal.xaml
    /// </summary>
    public partial class InfoResellerModal : Window
    {
        private DispatcherTimer timer;
        public InfoResellerModal(ResellerInfo resellerInfo)
        {
            InitializeComponent();
            this.DataContext = resellerInfo;
            timer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(4) };
            timer.Start();
            timer.Tick += Timer_Tick;
        }
        private void Timer_Tick(object sender, EventArgs e)
        {
            timer.Stop();
            timer.Tick -= Timer_Tick;
            this.Close();
        }
    }
}
