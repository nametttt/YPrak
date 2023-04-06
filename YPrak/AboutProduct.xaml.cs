using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
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
    /// Логика взаимодействия для AboutProduct.xaml
    /// </summary>
    public partial class AboutProduct : Window
    {
        public AboutProduct()
        {
            InitializeComponent();
        }
        
        private void Window_Closed(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            if (button.Content.ToString() == "+")
            {
                int c1 = Convert.ToInt32(Count.Text);
                c1++;
                Count.Text = c1.ToString();
            }
            else if (button.Content.ToString() == "-" && Convert.ToInt32(Count.Text) > 0)
            {
                int c1 = Convert.ToInt32(Count.Text);
                c1--;
                Count.Text = c1.ToString();
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            string[] Atr=null;
            int count = 0;
            using (prak1Entities1 prak = new prak1Entities1())
            {
                foreach (Product product in prak.Product)
                {
                     Atr = plow.Text.Split(' ');
                    if (Atr[1] == product.Product_Id)
                    {
                        count = Convert.ToInt32(Count.Text);
                        product.Count = count;
                    }
                }
                prak.SaveChanges();
                MessageBox.Show("Изделие добавлено в заказ!");
                this.Hide();
            }


            /// Получение активного окна и обновление таблицы
            foreach (CustomerWindow window in Application.Current.Windows.OfType<CustomerWindow>())
            {
                int sum =Convert.ToInt32(window.AllCost.Text);
                window.UpdateIz(1);

                /// Получение стоимости изделелий
                using (prak1Entities1 prak = new prak1Entities1())
                {
                    foreach(var fyfr in prak.Fyr_Product)
                    {
                        if (fyfr.Product_Id == Atr[1])
                        {
                            foreach (var tan in prak.Fyr)
                            {
                                if (fyfr.Fyr_Id == tan.Fyr_Id)
                                {
                                   sum+= tan.Cost*fyfr.Count;
                                }
                            }
                        }
                    }

                    foreach (var fyfr in prak.Textile_Product)
                    {
                        if (fyfr.Product_Id == Atr[1])
                        {
                            foreach (var tan in prak.Textile)
                            {
                                if (fyfr.Textile_Id == tan.Textile_Id)
                                {
                                    sum += tan.Cost*fyfr.Count;
                                }
                            }
                        }
                    }
                }
                    sum *= count;
                    window.AllCost.Text = sum.ToString();
            }
        }
    }
}
