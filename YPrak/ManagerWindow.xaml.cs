using DocumentFormat.OpenXml.Drawing.Charts;
using DocumentFormat.OpenXml.Vml;
using System;
using System.Collections.Generic;
using System.Data;
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
using System.Drawing;

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
            UpdateMyProduct();
        }

        public void UpdateMyProduct()
        {
            datagrid4.Items.Refresh();
            using (prak1Entities1 prak = new prak1Entities1())
            {
                var query1 = from Product in prak.Product
                             join textile_Product in prak.Textile_Product on Product.Product_Id equals textile_Product.Product_Id
                             join textile in prak.Textile on textile_Product.Textile_Id equals textile.Textile_Id
                             select new
                             {
                                 Артикул = Product.Product_Id,
                                 Название = Product.Name,
                                 Ширина = Product.Width,
                                 Длина = Product.Lenght,
                                 Ткань = textile.Name
                             };
                datagrid4.ItemsSource = query1.ToList();
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

        public void UpdateIz(double edin)
        {
            datagrid11.ItemsSource = null;
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
                datagrid11.ItemsSource = query1.ToList();
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
            datagrid2.ItemsSource = null;
            datagrid3.ItemsSource = null;
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
                datagrid3.ItemsSource = query1.ToList();
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

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            using (prak1Entities1 prak = new prak1Entities1())
            {
                foreach (User user in prak.User)
                {
                    Customer.Items.Add(user);
                }
            }
        }


        private void datagrid3_MouseDown(object sender, MouseButtonEventArgs e)
        {
            using (prak1Entities1 prak = new prak1Entities1())
            {
                int ordCount = 0, minus = 0, fabryc = 0;
                var selectedItems = datagrid3.SelectedItems;

                if (selectedItems.Count > 0)
                {
                    var selectedItem = selectedItems[0];

                    var selectedRow = (DataGridRow)datagrid3.ItemContainerGenerator.ContainerFromItem(selectedItem);

                    int selectedIndex = datagrid3.ItemContainerGenerator.IndexFromContainer(selectedRow);

                    var cells = datagrid3.Columns.Select(column =>
                    {
                        var cellContent = column.GetCellContent(selectedRow);
                        return (cellContent as TextBlock)?.Text;
                    }).ToList();

                    var number = cells[0];

                    IdFyr.Text = number;

                    var orderedProduct = prak.Ordered_Products.FirstOrDefault(i => i.Order_Id.ToString() == number);

                    if (orderedProduct != null)
                    {
                        ordCount = orderedProduct.Count;

                        var fyrProduct = prak.Fyr_Product.FirstOrDefault(j => j.Product_Id == orderedProduct.Product_Id);
                        var fabricFyr = prak.Fabric_Fyr.FirstOrDefault(x => x.Fyr_Id == fyrProduct.Fyr_Id);

                        if (fyrProduct != null)
                        {
                            minus = fyrProduct.Count;
                            fabryc = fabricFyr.Count;
                        }
                    }

                    if (ordCount * minus > fabryc)
                    {
                        DoFyr.Text = (fabryc / minus).ToString();
                        IsFyr.Background = Brushes.Red;
                    }
                    else
                    {
                        BrushConverter bc = new BrushConverter();
                        IsFyr.Background = (Brush)bc.ConvertFrom("#FFB5D5CA");
                    }
                    DoFyr.Text = (ordCount * minus).ToString();
                    IsFyr.Text = fabryc.ToString();
                }
            }
        }

        private void button_Click_2(object sender, RoutedEventArgs e)
        {
            bool canCutAll = false;
            Cutting ct = new Cutting();
            int w = 0;
            int h = 0;
            foreach (var item in datagrid4.Items)
            {
                var dataRowView = item as DataRowView;
                if (dataRowView != null)
                {
                    // Обработка данных строки
                    List<Tovar> products = new List<Tovar>();
                    var prodId = dataRowView[0].ToString();
                    var nam = dataRowView[1].ToString();
                    var wid = Convert.ToInt32(dataRowView[2]);
                    var len = Convert.ToInt32(dataRowView[3]);
                    using (prak1Entities1 db = new prak1Entities1())
                    {
                        foreach (var i in db.Textile_Product)
                        {
                            if (i.Product_Id == prodId)
                            {
                                foreach (var j in db.Textile)
                                {
                                    if (j.Textile_Id == i.Textile_Id)
                                    {
                                        w = j.Width;
                                        h = j.Lenght;
                                        Fabric fabric = new Fabric()
                                        {
                                            Width = (int)w,
                                            Length = (int)h,
                                            Cuts = new List<Cut> { new Cut { Width = 100, Length = 100 } }
                                        };
                                        Tovar t = new Tovar()
                                        {
                                            Name = nam,
                                            Width = wid,
                                            Length = len,
                                        };
                                        products.Add(t);
                                        canCutAll = ct.CutFabric(fabric, products);
                                    };
                                }
                            }
                        }
                    }
                }
            }
            if (canCutAll)
            {
                MessageBox.Show("Ткани хватит на все изделия!");
            }
            else
            {
                MessageBox.Show("Ткани не хватит на все изделия!");
            }
        }

        private void TextileName_TextChanged(object sender, TextChangedEventArgs e)
        {
            datagrid4.Items.Refresh();
            using (prak1Entities1 prak = new prak1Entities1())
            {
                var query1 = from Product in prak.Product
                             join textile_Product in prak.Textile_Product on Product.Product_Id equals textile_Product.Product_Id
                             join textile in prak.Textile on textile_Product.Textile_Id equals textile.Textile_Id
                             where textile.Name.ToLower().Contains(TextileName.Text)
                             select new
                             {
                                 Артикул = Product.Product_Id,
                                 Название = Product.Name,
                                 Ширина = Product.Width,
                                 Длина = Product.Lenght,
                                 Ткань = textile.Name
                             };
                datagrid4.ItemsSource = query1.ToList();
            }
        }
    }
}
