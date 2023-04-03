﻿using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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
using Path = System.IO.Path;

namespace YPrak
{
    /// <summary>
    /// Логика взаимодействия для DirectoryWindow.xaml
    /// </summary>
    public partial class DirectoryWindow : Window
    {
        public DirectoryWindow()
        {
            InitializeComponent(); UpdateOstFyr();
            UpdateOstTkani();
            UpdateSpTkani();
            UpdateSpFyr();
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

        private void UpdateOstFyr()
        {
            using (prak1Entities1 prak = new prak1Entities1())
            {
                var query1 = from Fyr in prak.Fyr
                             join Fabric_Fyr in prak.Fabric_Fyr on Fyr.Fyr_Id equals Fabric_Fyr.Fyr_Id
                             select new
                             {
                                 Артикул = Fyr.Fyr_Id,
                                 Название = Fyr.Name,
                                 Партия = Fabric_Fyr.Party,
                                 Количество = Fabric_Fyr.Count
                             };
                datagrid1.ItemsSource = query1.ToList();
            }
        }

        private void UpdateOstTkani()
        {
            using (prak1Entities1 prak = new prak1Entities1())
            {
                var query1 = from Textile in prak.Textile
                             join Fabric_Textile in prak.Fabric_Textile on Textile.Textile_Id equals Fabric_Textile.Textile_Id
                             select new
                             {
                                 Артикул = Textile.Textile_Id,
                                 Название = Textile.Name,
                                 Рулон = Fabric_Textile.Roll,
                                 Длина = Fabric_Textile.Lenght
                             };
                datagrid2.ItemsSource = query1.ToList();
            }
        }

        private void UpdateSpTkani()
        {
            using (prak1Entities1 prak = new prak1Entities1())
            {
                var query1 = from Textile in prak.Textile
                             join Picture in prak.Picture on Textile.Picture_Id equals Picture.Picture_Id
                             join Textile_Color in prak.Textile_Color on Textile.Textile_Id equals Textile_Color.Textile_Id
                             join Color in prak.Color on Textile_Color.Color_Id equals Color.Color_Id
                             join Textile_Consist in prak.Textile_Consist on Textile.Textile_Id equals Textile_Consist.Textile_Id
                             join Consist in prak.Consist on Textile_Consist.Consist_Id equals Consist.Consist_Id
                             join Fabric_Textile in prak.Fabric_Textile on Textile.Textile_Id equals Fabric_Textile.Textile_Id
                             where Fabric_Textile.Lenght == 0
                             select new
                             {
                                 Артикул = Textile.Textile_Id,
                                 Название = Textile.Name,
                                 Рисунок = Picture.Picture1,
                                 Состав = Consist.Consist1,
                                 Цвет = Color.Color1,
                                 Количество = Fabric_Textile.Lenght
                             };
                datagrid3.ItemsSource = query1.ToList();
            }
        }

        private void UpdateSpFyr()
        {
            using (prak1Entities1 prak = new prak1Entities1())
            {
                var query1 = from Fyr in prak.Fyr
                             join Fyr_Type in prak.Fyr_Type on Fyr.Fyr_Id equals Fyr_Type.Fyr_Id
                             join Type in prak.Type on Fyr_Type.Type_Id equals Type.Type_Id
                             join Fabric_Fyr in prak.Fabric_Fyr on Fyr.Fyr_Id equals Fabric_Fyr.Fyr_Id
                             where Fabric_Fyr.Count == 0
                             select new
                             {
                                 Артикул = Fyr.Fyr_Id,
                                 Название = Fyr.Name,
                                 Тип = Type.Type1,
                                 Количество = Fabric_Fyr.Count
                             };
                datagrid4.ItemsSource = query1.ToList();
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            UpdateOstFyr();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Отправлено на печать!");
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            UpdateOstTkani();
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            UpdateSpTkani();
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            UpdateSpFyr();
        }
        private void Save_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Text Files (*.txt)|*.txt|RichText Files (*.rtf)|*.rtf|XAML Files (*.xaml)|*.xaml|All files (*.*)|*.*";
            if (Convert.ToBoolean(sfd.ShowDialog()) == true)
            {
                TextRange doc = new TextRange(docBox.Document.ContentStart, docBox.Document.ContentEnd);
                using (FileStream fs = File.Create(sfd.FileName))
                {
                    if (System.IO.Path.GetExtension(sfd.FileName).ToLower() == ".rtf")
                        doc.Save(fs, DataFormats.Rtf);
                    else if (Path.GetExtension(sfd.FileName).ToLower() == ".txt")
                        doc.Save(fs, DataFormats.Text);
                    else
                        doc.Save(fs, DataFormats.Xaml);
                }
            }
        }

        private void Load_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "RichText Files (*.rtf)|*.rtf|All files (*.*)|*.*";

            if (Convert.ToBoolean(ofd.ShowDialog()) == true)
            {
                TextRange doc = new TextRange(docBox.Document.ContentStart, docBox.Document.ContentEnd);
                using (FileStream fs = new FileStream(ofd.FileName, FileMode.Open))
                {
                    if (Path.GetExtension(ofd.FileName).ToLower() == ".rtf")
                        doc.Load(fs, DataFormats.Rtf);
                    else if (Path.GetExtension(ofd.FileName).ToLower() == ".txt")
                        doc.Load(fs, DataFormats.Text);
                    else
                        doc.Load(fs, DataFormats.Xaml);
                }
            }
        }
    }
}