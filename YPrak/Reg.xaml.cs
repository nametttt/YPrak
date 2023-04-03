using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
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
    /// Логика взаимодействия для Reg.xaml
    /// </summary>
    public partial class Reg : Window
    {
        public Reg()
        {
            InitializeComponent();
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
                    if (Login.Text == "" || Password.Password == "")
                    {
                        MessageBox.Show("Заполните все данные!");
                    }
                    else
                    {
                        User user = new User()
                        {
                            Login = Login.Text,
                            Role = "Заказчик",
                            Password = this.GetHashString(Password.Password)
                        };
                        prak.User.Add(user);
                        prak.SaveChanges();
                        MessageBox.Show($"{user.Login}, регистрация прошла успешно.", "Success!");
                    }
                }
                catch
                {
                    MessageBox.Show("Произошла ошибка!");
                }
            }
        }
    }
}