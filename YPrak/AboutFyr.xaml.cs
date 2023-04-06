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
    /// Логика взаимодействия для AboutFyr.xaml
    /// </summary>
    public partial class AboutFyr : Window
    {
        public AboutFyr()
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
                foreach (Fyr fyr in prak.Fyr)
                {
                    if (plow.Text == fyr.Fyr_Id)
                    {
                        fyr.Name = name.Text;
                        fyr.Lenght = Convert.ToDouble(height.Text);
                        fyr.Width = Convert.ToDouble(width.Text);
                        fyr.Cost = Convert.ToInt32(cost.Text);
                    }
                }
                prak.SaveChanges();
                MessageBox.Show("Фурнитура изменена!");
                this.Hide();
            }
            foreach (StorekeeperWindow window in Application.Current.Windows.OfType<StorekeeperWindow>())
            {
                window.UpdateFyr(1);
            }
        }
    }
}
