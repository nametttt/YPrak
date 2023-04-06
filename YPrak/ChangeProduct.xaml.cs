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
    /// Логика взаимодействия для ChangeProduct.xaml
    /// </summary>
    public partial class ChangeProduct : Window
    {
        public ChangeProduct()
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
                foreach (Product fyr in prak.Product)
                {
                    if (plow.Text == fyr.Product_Id)
                    {
                        fyr.Name = name.Text;
                        fyr.Lenght = Convert.ToInt32(height.Text);
                        fyr.Width = Convert.ToInt32(width.Text);
                    }
                }
                prak.SaveChanges();
                MessageBox.Show("Изделие изменено!");
                this.Hide();
            }

            /// Получение активного окна и обновление таблицы
            foreach (ManagerWindow window in Application.Current.Windows.OfType<ManagerWindow>())
            {
                window.UpdateIz(1);
            }
        }
    }
}
