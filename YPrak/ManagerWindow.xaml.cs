using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
            InitializeComponent();
            using (prak1Entities1 prak = new prak1Entities1())
            {
                foreach (Product product in prak.Product)
                {
                    product.Count = 0;
                }
                prak.SaveChanges();
            }
            Edin1.SelectedIndex = 1;
            Edin.SelectedIndex = 1;
            UpdateZakaz();
        }

        private void OrderSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (OrderSearch.Text != "")
                {
                    string s = OrderSearch.Text;

                    datagrid1.Items.Refresh();
                    using (prak1Entities1 prak = new prak1Entities1())
                    {
                        var query1 = from Product in prak.Product
                                     select new
                                     {
                                         Артикул = Product.Product_Id,
                                         Название = Product.Name,
                                         Ширина = Product.Width,
                                         Длина = Product.Lenght,
                                         Количество = Product.Count
                                     };
                        datagrid1.ItemsSource = query1.ToList();
                        OrderSearch.Text = "";
                    }
                }
                else
                {
                    MessageBox.Show("Изделий не найдено!");
                }
            }
        }

        public void UpdateIz(double edin)
        {
            datagrid1.ItemsSource = null;
            using (prak1Entities1 prak = new prak1Entities1())
            {

                var query1 = from Product in prak.Product
                             select new
                             {
                                 Артикул = Product.Product_Id,
                                 Название = Product.Name,
                                 Ширина = Product.Width * edin,
                                 Длина = Product.Lenght * edin
                             };
                datagrid1.ItemsSource = query1.ToList();
            }
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


        public void UpdateZakaz()
        {
            datagrid2.ItemsSource=null;
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

                    datagrid11.Items.Refresh();
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
                        datagrid11.ItemsSource = query1.ToList();
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

        private void datagrid1_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            var selectedItems = datagrid11.SelectedItems;

            if (selectedItems.Count > 0)
            {
                var selectedItem = selectedItems[0];

                var selectedRow = (DataGridRow)datagrid11.ItemContainerGenerator.ContainerFromItem(selectedItem);

                int selectedIndex = datagrid11.ItemContainerGenerator.IndexFromContainer(selectedRow);

                var cells = datagrid11.Columns.Select(column =>
                {
                    var cellContent = column.GetCellContent(selectedRow);
                    return (cellContent as TextBlock)?.Text;
                }).ToList();

                var cellValue = cells[0];
                var cellText = cells[1];
                var cellValueText = cells[2];
                var cellWidth = cells[3];
                AboutProduct product = new AboutProduct();
                product.Show();
                product.plow.Text += cellValue;
                product.name.Text += cellText;
                product.width.Text += cellWidth;
                product.height.Text += cellValueText;

                var s = cellValue;
                Image image = new Image();
                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.UriSource = new Uri($"ProdImage/{s}.JPG", UriKind.Relative);
                bitmap.EndInit();
                product.image.Source = bitmap;
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
            string sql = "";
            string connectionString = "Data Source=LAPTOP-KH7CD52O;Initial Catalog=prak1;Integrated security=True";
            using (prak1Entities1 prak = new prak1Entities1())
            {
                List<Order> orders = new List<Order>();
                List<Ordered_Products> products = new List<Ordered_Products>();
                User selectedUser = null;
                foreach (User user in prak.User)
                {
                    selectedUser = user;
                    break;
                }

                foreach (var i in prak.Product)
                {
                    if (Convert.ToInt32(i.Count) > 0)
                    {

                        SqlConnection connection = new SqlConnection(connectionString);
                        Order order = new Order()
                        {
                            Date = DateTime.Now,
                            Status = "Новый",
                            Customer = "Я",
                            Manager = "3"
                        };

                        prak.Order.Add(order);

                        Ordered_Products ordered = new Ordered_Products()
                        {
                            Order_Id = order.Order_Id,
                            Product_Id = i.Product_Id.ToString(),
                            Count = Convert.ToInt32(i.Count)
                        };
                        products.Add(ordered);

                    }

                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {

                        connection.Open();
                        foreach (Ordered_Products order in products)
                        {
                            sql = $"INSERT INTO \"Order\"VALUES   ({DateTime.Now}, \"Новый\", {Customer.Text}, \"3\")";
                            SqlCommand command = new SqlCommand(sql, connection);
                        }

                        foreach (Order ord in orders)
                        {
                            sql = $"INSERT INTO \"Order\"VALUES   ({DateTime.Now}, \"Новый\", {Customer.Text}, {Convert.ToInt32(i.Count)})";
                            SqlCommand command = new SqlCommand(sql, connection);

                        }
                    }

                }


                foreach (Product product in prak.Product)
                {
                    product.Count = 0;
                }
                UpdateIz(1);
                prak.SaveChanges();

                MessageBox.Show("Успешное оформление заказа!");
            }
        }

        private void datagrid1_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var selectedItems = datagrid1.SelectedItems;

            if (selectedItems.Count > 0)
            {
                var selectedItem = selectedItems[0];

                var selectedRow = (DataGridRow)datagrid1.ItemContainerGenerator.ContainerFromItem(selectedItem);

                int selectedIndex = datagrid1.ItemContainerGenerator.IndexFromContainer(selectedRow);

                var cells = datagrid1.Columns.Select(column =>
                {
                    var cellContent = column.GetCellContent(selectedRow);
                    return (cellContent as TextBlock)?.Text;
                }).ToList();

                var cellValue = cells[0];
                var cellText = cells[1];
                var cellValueText = cells[3];
                var cellWidth = cells[2];
                ChangeProduct product = new ChangeProduct();
                product.Show();
                product.plow.Text += cellValue;
                product.name.Text += cellText;
                product.width.Text += cellWidth;
                product.height.Text += cellValueText;

                var s = cellValue;
                Image image = new Image();
                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.UriSource = new Uri($"ProdImage/{s}.JPG", UriKind.Relative);
                bitmap.EndInit();
                product.image.Source = bitmap;
            }
        }

        private void datagrid2_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var selectedItems = datagrid2.SelectedItems;

            if (selectedItems.Count > 0)
            {
                var selectedItem = selectedItems[0];

                var selectedRow = (DataGridRow)datagrid2.ItemContainerGenerator.ContainerFromItem(selectedItem);

                int selectedIndex = datagrid2.ItemContainerGenerator.IndexFromContainer(selectedRow);

                var cells = datagrid2.Columns.Select(column =>
                {
                    var cellContent = column.GetCellContent(selectedRow);
                    return (cellContent as TextBlock)?.Text;
                }).ToList();

                var number = cells[0];
                var date = cells[1];
                var status = cells[2];
                var customer = cells[3];
                var manager = cells[4];

                ChangeOrder product = new ChangeOrder();
                product.Show();
                product.plow.Text = number;
                product.date.Text = date;
                product.status.Text = status;
                product.customer.Text = customer;
                product.manager.Text = manager;

            }

        }
    }
}
