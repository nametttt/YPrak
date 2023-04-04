using Microsoft.Win32;
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
    /// Логика взаимодействия для StorekeeperWindow.xaml
    /// </summary>
    public partial class StorekeeperWindow : Window
    {
        string textileId, fyrId;
        string textileName, fyrName;
        public StorekeeperWindow()
        {
            InitializeComponent();
            Edin1.SelectedIndex = 1;
            Edin2.SelectedIndex = 1;
        }

        private void UpdateTkani(double edin)
        {
            datagrid1.Items.Refresh();
            using (prak1Entities1 prak = new prak1Entities1())
            {
                var query1 = from Textile in prak.Textile
                             join Picture in prak.Picture on Textile.Picture_Id equals Picture.Picture_Id
                             join Textile_Color in prak.Textile_Color on Textile.Textile_Id equals Textile_Color.Textile_Id
                             join Color in prak.Color on Textile_Color.Color_Id equals Color.Color_Id
                             join Textile_Consist in prak.Textile_Consist on Textile.Textile_Id equals Textile_Consist.Textile_Id
                             join Consist in prak.Consist on Textile_Consist.Consist_Id equals Consist.Consist_Id
                             select new
                             {
                                 Артикул = Textile.Textile_Id,
                                 Название = Textile.Name,
                                 Рисунок = Picture.Picture1,
                                 Состав = Consist.Consist1,
                                 Цвет = Color.Color1,
                                 Ширина = Textile.Width * edin,
                                 Длина = Textile.Lenght * edin,
                                 Цена = Textile.Cost
                             };
                datagrid1.ItemsSource = query1.ToList();
            }
        }

        private void UpdateFyr(double edin)
        {
            datagrid2.Items.Refresh();

            using (prak1Entities1 prak = new prak1Entities1())
            {
                var query1 = from Fyr in prak.Fyr
                             join Fyr_Type in prak.Fyr_Type on Fyr.Fyr_Id equals Fyr_Type.Fyr_Id
                             join Type in prak.Type on Fyr_Type.Type_Id equals Type.Type_Id
                             select new
                             {
                                 Артикул = Fyr.Fyr_Id,
                                 Название = Fyr.Name,
                                 Тип = Type.Type1,
                                 Ширина = Fyr.Width * edin,
                                 Длина = Fyr.Lenght * edin,
                                 Цена = Fyr.Cost,
                                 Вес = Fyr.Weight
                             };
                datagrid2.ItemsSource = query1.ToList();
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

        private void Edin1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Edin1.SelectedIndex == 0)
            {
                UpdateTkani(10);
            }
            else if (Edin1.SelectedIndex == 2)
            {
                UpdateTkani(0.1);
            }
            else if (Edin1.SelectedIndex == 3)
            {
                UpdateTkani(0.01);
            }
            else if (Edin1.SelectedIndex == 1)
            {
                UpdateTkani(1);
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
                        var query1 = from Textile in prak.Textile
                                     join Picture in prak.Picture on Textile.Picture_Id equals Picture.Picture_Id
                                     join Textile_Color in prak.Textile_Color on Textile.Textile_Id equals Textile_Color.Textile_Id
                                     join Color in prak.Color on Textile_Color.Color_Id equals Color.Color_Id
                                     join Textile_Consist in prak.Textile_Consist on Textile.Textile_Id equals Textile_Consist.Textile_Id
                                     join Consist in prak.Consist on Textile_Consist.Consist_Id equals Consist.Consist_Id
                                     where Textile.Name.ToLower().Contains(s)
                                     select new
                                     {
                                         Артикул = Textile.Textile_Id,
                                         Название = Textile.Name,
                                         Рисунок = Picture.Picture1,
                                         Состав = Consist.Consist1,
                                         Цвет = Color.Color1,
                                         Ширина = Textile.Width,
                                         Длина = Textile.Lenght,
                                         Цена = Textile.Cost
                                     };
                        datagrid1.ItemsSource = query1.ToList();
                        Search1.Text = "";
                    }
                }
                else
                {
                    MessageBox.Show("Тканей не найдено!");
                }
            }
        }

        private void Edin2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Edin2.SelectedIndex == 0)
            {
                UpdateFyr(10);
            }
            else if (Edin2.SelectedIndex == 2)
            {
                UpdateFyr(0.1);
            }
            else if (Edin2.SelectedIndex == 3)
            {
                UpdateFyr(0.01);
            }
            else if (Edin2.SelectedIndex == 1)
            {
                UpdateFyr(1);
            }
        }

        private void Search2_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (Search2.Text != "")
                {
                    string s = Search2.Text;

                    datagrid2.Items.Refresh();
                    using (prak1Entities1 prak = new prak1Entities1())
                    {
                        var query1 = from Fyr in prak.Fyr
                                     join Fyr_Type in prak.Fyr_Type on Fyr.Fyr_Id equals Fyr_Type.Fyr_Id
                                     join Type in prak.Type on Fyr_Type.Type_Id equals Type.Type_Id
                                     where Fyr.Name.ToLower().Contains(s)
                                     select new
                                     {
                                         Артикул = Fyr.Fyr_Id,
                                         Название = Fyr.Name,
                                         Тип = Type.Type1,
                                         Ширина = Fyr.Width,
                                         Длина = Fyr.Lenght,
                                         Цена = Fyr.Cost,
                                         Вес = Fyr.Weight
                                     };
                        datagrid2.ItemsSource = query1.ToList();
                        Search2.Text = "";
                    }
                }
                else
                {
                    MessageBox.Show("Фурнитуры не найдено!");
                }
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {

            if (Convert.ToDouble(textbox2.Text) > 20)
                MessageBox.Show("Данные отправлены на утверждение Дирекцией");
            else
                MessageBox.Show("Данные приняты к учету");
            }
            catch
            {
                MessageBox.Show("Введите все данные!");
            }
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            combobox2.Items.Clear();
            using (prak1Entities1 prak = new prak1Entities1())
            {
                if (combobox1.SelectedIndex == 0)
                {
                    foreach (Textile textile in prak.Textile)
                    {
                        combobox2.Items.Add(textile.Name);
                    }
                }
                else if (combobox1.SelectedIndex == 1)
                {
                    foreach (Fyr fyr in prak.Fyr)
                    {
                        combobox2.Items.Add(fyr.Name);
                    }
                }
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            using (prak1Entities1 prak = new prak1Entities1())
            {
                foreach (Textile textile in prak.Textile)
                {
                    sptkani.Items.Add(textile.Name);
                    //posttkani.Items.Add(textile.Name);
                }
                foreach (Fyr fyr in prak.Fyr)
                {
                    spfyr.Items.Add(fyr.Name);
                    //postfyr.Items.Add(fyr.Name);
                }

            }
        }

        private void combobox2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            using (prak1Entities1 prak = new prak1Entities1())
            {
                if (combobox1.SelectedIndex == 1)
                {
                    foreach (Fyr fyr in prak.Fyr)
                    {
                        fyrName = combobox2.SelectedItem.ToString();
                        if (fyrName == fyr.Name)
                        {
                            fyrId = fyr.Fyr_Id;

                            foreach (Fabric_Fyr count1 in prak.Fabric_Fyr)
                            {
                                if (fyrId == count1.Fyr_Id)
                                {
                                    sklad.Text = count1.Count.ToString();

                                }
                            }
                        }
                    }
                }
                else if (combobox1.SelectedIndex == 0)
                {
                    foreach (Textile textile in prak.Textile)
                    {
                        textileName = combobox2.SelectedItem.ToString();
                        if (textileName == textile.Name)
                        {
                            textileId = textile.Textile_Id;

                            foreach (Fabric_Textile count1 in prak.Fabric_Textile)
                            {
                                if (textileId == count1.Textile_Id)
                                {
                                    sklad.Text = count1.Roll.ToString();
                                }
                            }
                        }
                    }
                }
            }
        }

        private void textbox_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                using (prak1Entities1 prak = new prak1Entities1())
                {
                    try
                    {
                        if (combobox1.SelectedIndex == 0)
                        {

                            foreach (Fabric_Textile count1 in prak.Fabric_Textile)
                            {
                                if (textileId == count1.Textile_Id)
                                {
                                    double changes;
                                    double x, y;
                                    x = Convert.ToInt32(count1.Roll);
                                    y = Convert.ToInt32(textbox1.Text);
                                    changes = ((x - y) / x) * 100;
                                    textbox2.Text = changes.ToString();
                                }
                            }
                        }
                        else if (combobox1.SelectedIndex == 1)
                        {

                            foreach (Fabric_Fyr count2 in prak.Fabric_Fyr)
                            {
                                if (fyrId == count2.Fyr_Id)
                                {
                                    double changes;
                                    double x, y;
                                    x = Convert.ToInt32(count2.Count);
                                    y = Convert.ToInt32(textbox1.Text);
                                    changes = ((x - y) / x) * 100;
                                    textbox2.Text = changes.ToString();
                                }

                            }
                        }
                    }
                    catch
                    {
                        MessageBox.Show("Произошла ошибка!");
                    }
                }
            }
        }
        private void SpFyr(object sender, RoutedEventArgs e)
        {
            try
            {
                using (prak1Entities1 prak = new prak1Entities1())
                {
                    if (Convert.ToString(spfyr.SelectedItem) == "")
                    {
                        MessageBox.Show("Введите данные!");
                    }
                    string fyrId = Convert.ToString(spfyr.SelectedItem);
                    foreach (Fabric_Fyr fab_fyr in prak.Fabric_Fyr)
                    {
                        if (fyrId == fab_fyr.Fyr_Id)
                        {
                            if (fab_fyr.Count == 0)
                                MessageBox.Show("Фурнитура уже была списана!");
                            else
                            {
                                fab_fyr.Count = 0;
                                MessageBox.Show("Фурнитура списана!");
                            }
                        }
                    }
                    prak.SaveChanges();
                    UpdateFyr(1);
                }
            }
            catch
            {
                MessageBox.Show("Произошла ошибка!");
            }
        }

        private void SpTkani(object sender, RoutedEventArgs e)
        {
            try
            {
                using (prak1Entities1 prak = new prak1Entities1())
                {
                    if (Convert.ToString(sptkani.SelectedItem) == "")
                    {
                        MessageBox.Show("Введите данные!");
                    }
                    string TextileId = Convert.ToString(sptkani.SelectedItem);
                    foreach (Fabric_Textile fab_textile in prak.Fabric_Textile)
                    {
                        if (TextileId == fab_textile.Textile_Id)
                        {
                            if (fab_textile.Roll == 0)
                                MessageBox.Show("Ткань уже была списана!");
                            else
                            {
                                fab_textile.Lenght = 0;
                                MessageBox.Show("Ткань списана!");
                            }
                        }
                    }
                    prak.SaveChanges();
                    UpdateTkani(1);
                }

            }
            catch
            {
                MessageBox.Show("Произошла ошибка!");
            }
        }

        private void PostNewFyr(object sender, RoutedEventArgs e)
        {
            //try
            //{
            //    if (fartic.Text != "" || fname.Text != "" || fweight.Text != "" || flenght.Text != "")
            //        using (prak1Entities1 prak = new prak1Entities1())
            //        {
            //            Fyr fyr = new Fyr()
            //            {
            //                Fyr_Id = fartic.Text,
            //                Name = fname.Text,
            //                Weight = Convert.ToInt32(fweight.Text),
            //                Lenght = Convert.ToInt32(flenght.Text)
            //            };
            //            prak.SaveChanges();
            //            MessageBox.Show("Фурнитура добавлена!");
            //        }
            //}
            //catch
            //{
            //    MessageBox.Show("Произошла ошибка!");
            //}
        }

        private void PostFyr(object sender, RoutedEventArgs e)
        {
            //try
            //{
            //    using (prak1Entities1 prak = new prak1Entities1())
            //    {
            //        string str = Convert.ToString(postfyr.SelectedItem);
            //        foreach (Fyr fab in prak.Fyr)
            //        {
            //            if (str == fab.Name)
            //                fyrId = fab.Fyr_Id;
            //        }
            //        int count = Convert.ToInt32(count1.Text);
            //        int cost = Convert.ToInt32(cost1.Text);
            //        int newcost = count * cost;
            //        allcost1.Text = newcost.ToString();
            //        foreach (Fabric_Fyr fab_fyr in prak.Fabric_Fyr)
            //        {
            //            if (fyrId == fab_fyr.Fyr_Id)
            //            {
            //                fab_fyr.Count += count;
            //            }
            //        }
            //        prak.SaveChanges();
            //        MessageBox.Show("Запись обновлена!");
            //    }
            //}
            //catch
            //{
            //    MessageBox.Show("Произошла ошибка!");
            //}
        }
        private void PostTkani(object sender, RoutedEventArgs e)
        {
            //try
            //{
            //    using (prak1Entities1 prak = new prak1Entities1())
            //    {
            //        string str = Convert.ToString(posttkani.SelectedItem);
            //        foreach (Textile textile in prak.Textile)
            //        {
            //            if (str == textile.Name)
            //                textileId = textile.Textile_Id;
            //        }
            //        int count1 = Convert.ToInt32(count.Text);
            //        int cost1 = Convert.ToInt32(cost.Text);
            //        int newcost = count1 * cost1;
            //        allcost.Text = newcost.ToString();
            //        foreach (Fabric_Textile fab_textile in prak.Fabric_Textile)
            //        {
            //            if (textileId == fab_textile.Textile_Id)
            //            {
            //                fab_textile.Roll += count1;
            //                fab_textile.Lenght += Convert.ToDouble(lenght.Text);
            //            }
            //        }
            //        prak.SaveChanges();
            //        MessageBox.Show("Запись обновлена!");
            //    }
            //}
            //catch
            //{
            //    MessageBox.Show("Запись обновлена!");
            //}
        }
    }
}
