using DocumentFormat.OpenXml.Drawing.Charts;
using DocumentFormat.OpenXml.Wordprocessing;
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
using System.Windows.Shapes;

namespace YPrak
{
    /// <summary>
    /// Логика взаимодействия для ChangeOrder.xaml
    /// </summary>
    public partial class ChangeOrder : Window
    {
        public ChangeOrder()
        {
            InitializeComponent();
        }
        private void Window_Closed(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            using (prak1Entities1 prak = new prak1Entities1())
            {
                foreach (Order order in prak.Order)
                {
                    if (plow.Text == order.Order_Id.ToString())
                    {
                        order.Date = Convert.ToDateTime(date.Text);
                        order.Status = status.Text;
                    }
                }
                prak.SaveChanges();
                MessageBox.Show("Заказ изменен!");
                this.Hide();
            }
            foreach (ManagerWindow window in Application.Current.Windows.OfType<ManagerWindow>())
            {
                window.UpdateZakaz();
            }
        }
    }
}

