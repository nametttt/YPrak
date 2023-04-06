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
    /// Логика взаимодействия для AboutTextile.xaml
    /// </summary>
    public partial class AboutTextile : Window
    {
        public AboutTextile()
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
                foreach (Textile textile in prak.Textile)
                {
                    if (plow.Text == textile.Textile_Id)
                    {
                        textile.Name = name.Text;
                        textile.Lenght = Convert.ToInt32(height.Text);
                        textile.Width = Convert.ToInt32(width.Text);
                        textile.Cost = Convert.ToInt32(cost.Text);
                    }
                }
                prak.SaveChanges();
                MessageBox.Show("Ткань изменена!");
                this.Hide();
            }
            foreach (StorekeeperWindow window in Application.Current.Windows.OfType<StorekeeperWindow>())
            {
                window.UpdateTkani(1);
            }
        }
    }
}
