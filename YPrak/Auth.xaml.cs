using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace YPrak
{
    /// <summary>
    /// Логика взаимодействия для Auth.xaml
    /// </summary>
    public partial class Auth : Window
    {
        public Auth()
        {
            InitializeComponent();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Reg reg = new Reg();
            this.Hide();
            reg.Show();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            Application.Current.Shutdown();
        }
        private string GetHashString(string s)
        {
            byte[] bytes = Encoding.Unicode.GetBytes(s);

            MD5CryptoServiceProvider CSP = new MD5CryptoServiceProvider();
            byte[] byteHash = CSP.ComputeHash(bytes);
            string hash = "";
            foreach (byte b in byteHash)
            {
                hash += string.Format("{0:x2}", b);
            }
            return hash;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            using (prak1Entities1 prak = new prak1Entities1())
            {
                try
                {
                    foreach (User user in prak.User)
                    {
                        if (Login.Text == user.Login && this.GetHashString(Password.Password) == user.Password && user.Role == "Заказчик")
                        {
                            MessageBox.Show($"Добро пожаловать, {user.Login}!", "Success!");
                            CustomerWindow order = new CustomerWindow();
                            order.Show();
                            this.Hide();
                            return;
                        }

                        else if (Login.Text == user.Login && Password.Password == user.Password && user.Role == "Менеджер")
                        {
                            MessageBox.Show($"Добро пожаловать, {user.Login}!", "Success!");
                            ManagerWindow manager = new ManagerWindow();
                            manager.Show();
                            this.Hide();
                            return;
                        }
                        else if (Login.Text == user.Login && Password.Password == user.Password && user.Role == "Дирекция")
                        {
                            MessageBox.Show($"Добро пожаловать, {user.Login}!", "Success!");
                            DirectoryWindow directory = new DirectoryWindow();
                            directory.Show();
                            this.Hide();
                            return;
                        }
                        else if (Login.Text == user.Login && Password.Password == user.Password && user.Role == "Кладовщик")
                        {
                            MessageBox.Show($"Добро пожаловать, {user.Login}!", "Success!");
                            StorekeeperWindow store = new StorekeeperWindow();
                            store.Show();
                            this.Hide();
                            return;
                        }
                    }
                }
                catch
                {
                    MessageBox.Show("Произошла ошибка!");
                }
                MessageBox.Show("Произошла ошибка!");
            }
        }
    }
}
