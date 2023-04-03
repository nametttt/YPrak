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
    /// Логика взаимодействия для ManagerWindow.xaml
    /// </summary>
    public partial class ManagerWindow : Window
    {
        public ManagerWindow()
        {
            InitializeComponent(); Edin.SelectedIndex = 1;
            UpdateZakaz();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Auth main = new Auth();
            this.Hide();
            main.Show();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void UpdateIz(double edin)
        {
            datagrid1.Items.Refresh();
            using (prak1Entities1 prak = new prak1Entities1())
            {
                var query1 = from Product in prak.Product
                             select new
                             {
                                 Артикул = Product.Product_Id,
                                 Название = Product.Name,
                                 Ширина = Product.Width,
                                 Длина = Product.Lenght * edin
                             };
                datagrid1.ItemsSource = query1.ToList();
            }
        }

        private void UpdateZakaz()
        {
            datagrid2.Items.Refresh();
            using (prak1Entities1 prak = new prak1Entities1())
            {
                var query1 = from Order in prak.Order
                             select new
                             {
                                 Номер = Order.Order_Id,
                                 Дата = Order.Date,
                                 Статус = Order.Status,
                                 Покупатель = Order.Customer,
                                 Менеджер = Order.Manager
                             };
                datagrid2.ItemsSource = query1.ToList();
            }
        }

        private void Edin1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Edin.SelectedIndex == 0)
            {
                UpdateIz(10);
            }
            else if (Edin.SelectedIndex == 2)
            {
                UpdateIz(0.1);
            }
            else if (Edin.SelectedIndex == 3)
            {
                UpdateIz(0.01);
            }
            else if (Edin.SelectedIndex == 1)
            {
                UpdateIz(1);
            }
        }

        private void Search1_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (Search1.Text != "")
                {
                    string s = Search1.Text;

                    datagrid1.Items.Refresh();
                    using (prak1Entities1 prak = new prak1Entities1())
                    {
                        var query1 = from Product in prak.Product
                                     where Product.Name.ToLower().Contains(s)
                                     select new
                                     {
                                         Артикул = Product.Product_Id,
                                         Название = Product.Name,
                                         Ширина = Product.Width,
                                         Длина = Product.Lenght
                                     };
                        datagrid1.ItemsSource = query1.ToList();
                        Search1.Text = "";
                    }
                }
                else
                {
                    MessageBox.Show("Изделий не найдено!");
                }
            }
        }

        private void Search3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (Search3.Text != "")
                {
                    string s = Search3.Text;

                    datagrid2.Items.Refresh();
                    using (prak1Entities1 prak = new prak1Entities1())
                    {
                        var query1 = from Order in prak.Order
                                     where Order.Customer.ToLower().Contains(s)
                                     select new
                                     {
                                         Номер = Order.Order_Id,
                                         Дата = Order.Date,
                                         Статус = Order.Status,
                                         Покупатель = Order.Customer,
                                         Менеджер = Order.Manager
                                     };
                        datagrid2.ItemsSource = query1.ToList();
                        Search3.Text = "";
                    }
                }
                else
                {
                    MessageBox.Show("Заказов не найдено!");
                }
            }
        }

        private void Search4_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (Search4.Text != "")
                {
                    string s = Search4.Text;

                    datagrid2.Items.Refresh();
                    using (prak1Entities1 prak = new prak1Entities1())
                    {
                        var query1 = from Order in prak.Order
                                     where Order.Manager.ToLower().Contains(s)
                                     select new
                                     {
                                         Номер = Order.Order_Id,
                                         Дата = Order.Date,
                                         Статус = Order.Status,
                                         Покупатель = Order.Customer,
                                         Менеджер = Order.Manager
                                     };
                        datagrid2.ItemsSource = query1.ToList();
                        Search4.Text = "";
                    }
                }
                else
                {
                    MessageBox.Show("Заказов не найдено!");
                }
            }
        }

        private void Search2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (Search2.Text != "")
                {
                    int s = Convert.ToInt32(Search2.Text);

                    datagrid2.Items.Refresh();
                    using (prak1Entities1 prak = new prak1Entities1())
                    {
                        var query1 = from Order in prak.Order
                                     where Order.Order_Id == s
                                     select new
                                     {
                                         Номер = Order.Order_Id,
                                         Дата = Order.Date,
                                         Статус = Order.Status,
                                         Покупатель = Order.Customer,
                                         Менеджер = Order.Manager
                                     };
                        datagrid2.ItemsSource = query1.ToList();
                        Search2.Text = "";
                    }
                }
                else
                {
                    MessageBox.Show("Заказов не найдено!");
                }
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            UpdateZakaz();
        }
    }
}

