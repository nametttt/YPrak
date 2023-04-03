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
    /// Логика взаимодействия для CustomerWindow.xaml
    /// </summary>
    public partial class CustomerWindow : Window
    {
        public CustomerWindow()
        {
            InitializeComponent(); UpdateIz();
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
        private void UpdateIz()
        {
            int count = 0;
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
                                 Количество = count
                             };
                datagrid1.ItemsSource = query1.ToList();
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Заказ оформлен!");
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
                bitmap.UriSource = new Uri($"prod/{s}.JPG", UriKind.Relative);
                bitmap.EndInit();
                product.image.Source = bitmap;
            }
        }
    }
}
