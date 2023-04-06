using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
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
    /// Логика взаимодействия для CustomerWindow.xaml
    /// </summary>
    public partial class CustomerWindow : Window
    {

        public CustomerWindow()
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
            UpdateMyIz();

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
                                     where Order.Order_Id == s && Order.Customer == "Логин"
                                     select new
                                     {
                                         Номер = Order.Order_Id,
                                         Дата = Order.Date,
                                         Статус = Order.Status,
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

        public void UpdateMyIz()
        {
            datagrid2.ItemsSource = null;
            using (prak1Entities1 prak = new prak1Entities1())
            {

                var query1 = from Order in prak.Order
                             where Order.Customer == "Я"
                             select new
                             {
                                 Номер = Order.Order_Id,
                                 Дата = Order.Date,
                                 Статус = Order.Status,
                                 Менеджер = Order.Manager
                             };
                datagrid2.ItemsSource = query1.ToList();
            }
        }

            private void Window_Closed(object sender, EventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Auth main = new Auth();
            this.Hide();
            main.Show();
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
                                 Длина = Product.Lenght * edin,
                                 Количество = Product.Count
                             };
                datagrid1.ItemsSource = query1.ToList();
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
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
                                sql = $"INSERT INTO \"Order\"VALUES   ({DateTime.Now}, \"Новый\", {selectedUser.Login}, \"3\")";
                                SqlCommand command = new SqlCommand(sql, connection);
                            }

                            foreach (Order ord in orders)
                            {
                                sql = $"INSERT INTO \"Order\"VALUES   ({DateTime.Now}, \"Новый\", {selectedUser.Login}, {Convert.ToInt32(i.Count)})";
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
        }

    private void datagrid1_PreviewMouseDown(object sender, MouseButtonEventArgs e)
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

        private void Edin1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Edin1.SelectedIndex == 0)
            {
                UpdateIz(10);
            }
            else if (Edin1.SelectedIndex == 2)
            {
                UpdateIz(0.1);
            }
            else if (Edin1.SelectedIndex == 3)
            {
                UpdateIz(0.01);
            }
            else if (Edin1.SelectedIndex == 1)
            {
                UpdateIz(1);
            }
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
    }
}
