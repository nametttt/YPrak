﻿using System;
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
            Application.Current.Shutdown();
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
    }
}