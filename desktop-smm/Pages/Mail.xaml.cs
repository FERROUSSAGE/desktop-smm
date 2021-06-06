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
    /// Логика взаимодействия для Mail.xaml
    /// </summary>
    public partial class Mail : Page
    {
        public Mail()
        {
            InitializeComponent();
            incorrectPattern.Tag = "Здравствуйте, Ваш заказ<strong>#Номер заказа</strong> не может быть исполнен, поскольку в нём указана не корректная ссылка для продвижения. Чтобы Ваш заказ был исполнен отпишитесь, пожалуйста в чат поддержки на нашем сайте!)<br><br>\n\n\n С уважением,<br>команда <strong>SmmCraft.Ru</strong><br> <strong>E - mail: </strong> <a href=\"mailto:team@smmcraft.ru\">team@smmcraft.ru</a><br> <strong>Сайт: </strong><a href=\"https://smmcraft.ru/\">smmcraft.ru</a><br>";
            endDayPattern.Tag = "Здравствуйте, Ваш заказ <strong>#Номер заказа</strong> был оформлен после окончания рабочей смены оператора. Чтобы запустить Ваш заказ отпишитесь, пожалуйста оператору в чат поддержки на нашем сайте!)<br><br>\n\n\n С уважением,<br>команда <strong>SmmCraft.Ru</strong><br> <strong>E - mail: </strong> <a href=\"mailto:team@smmcraft.ru\">team@smmcraft.ru</a><br> <strong>Сайт: </strong><a href=\"https://smmcraft.ru/\">smmcraft.ru</a><br>";

            foreach (var item in gridButtons.Children)
            {
                var button = (item as Border).Child as TextBlock;

                button.MouseDown += (s, e) =>
                {
                    foreach (var j in gridButtons.Children)
                        ((j as Border).Child as TextBlock).Foreground = (Brush)Application.Current.Resources["ColorMailText"];

                    button.Foreground = (Brush)Application.Current.Resources["ColorButtonActive"];
                    tbHtml.Text = (button.Parent as Border).Tag.ToString();
                };
            }
        }
    }
}
