
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Microsoft.Office.Interop.Word;
using Table = Microsoft.Office.Interop.Word.Table;
using Document = Microsoft.Office.Interop.Word.Document;
using Window = System.Windows.Window;
using Application = Microsoft.Office.Interop.Word.Application;
using System.IO;
using System.Windows.Media.Imaging;

namespace YPrak
{
    /// <summary>
    /// Логика взаимодействия для StorekeeperWindow.xaml
    /// </summary>
    public partial class StorekeeperWindow : Window
    {
        string textileId, fyrId;
        int textilePicture;
        string textileName, fyrName;
        private List<Textile> PostTextile = new List<Textile>();
        public StorekeeperWindow()
        {
            InitializeComponent();
            Edin1.SelectedIndex = 1;
            Edin2.SelectedIndex = 1;
        }


        public void UpdateTkani(double edin)
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

        public void UpdateFyr(double edin)
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
            System.Windows.Application.Current.Shutdown();
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
            combobox1.Text = string.Empty;
            combobox2.Text = string.Empty;
            sklad.Text = string.Empty;
            textbox1.Text = string.Empty;
            textbox2.Text = string.Empty;
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
                foreach (Picture pic in prak.Picture)
                {
                    textilPicture.Items.Add(pic.Picture1);
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
                            }
                        }
                    }
                    MessageBox.Show("Фурнитура списана!");
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
                            }
                        }
                    }
                    MessageBox.Show("Ткань списана!");
                    prak.SaveChanges();
                    UpdateTkani(1);
                }

            }
            catch
            {
                MessageBox.Show("Произошла ошибка!");
            }
        }

        private void fyrnCount_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (fyrnCost.Text != "" && fyrnCount.Text != "")
                {
                    int count = Convert.ToInt32(fyrnCount.Text);
                    int cost = Convert.ToInt32(fyrnCost.Text);
                    int newcost = count * cost;
                    fyrnAllCost.Text = newcost.ToString();
                }
            }
            catch
            {
                MessageBox.Show("Произошла ошибка!");
            }
        }

        private void textilCount_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (textilCost.Text != "" && textilCount.Text != "")
                {
                    int count = Convert.ToInt32(textilCount.Text);
                    int cost = Convert.ToInt32(textilCost.Text);
                    int newcost = count * cost;
                    textilAllCost.Text = newcost.ToString();
                }
            }
            catch
            {
                MessageBox.Show("Произошла ошибка!");
            }
        }

        private void textbox1_TextChanged(object sender, TextChangedEventArgs e)
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

        private void PostFyr(object sender, RoutedEventArgs e)
        {
            try
            {
                if (fyrnId.Text != "" || fyrnName.Text != "" || fyrnWidth.Text != "" || fyrnLenght.Text != "" || fyrnCost.Text != "" || fyrnCount.Text != "" || fyrnWeight.Text != "")
                    using (prak1Entities1 prak = new prak1Entities1())
                    {
                        Fyr fyr = new Fyr()
                        {
                            Fyr_Id = fyrnId.Text,
                            Name = fyrnName.Text,
                            Weight = Convert.ToInt32(fyrnWeight.Text),
                            Lenght = Convert.ToInt32(fyrnLenght.Text),
                            Width = Convert.ToInt32(fyrnWidth.Text),
                            Cost = Convert.ToInt32(fyrnCost.Text)
                        };
                        prak.Fyr.Add(fyr);

                        Fabric_Fyr fabric = new Fabric_Fyr()
                        { 
                            Fyr_Id = fyrnId.Text,
                            Count = Convert.ToInt32(fyrnCount.Text)
                        };
                        prak.Fabric_Fyr.Add(fabric);
                        CreateWordDocument();
                        prak.SaveChanges();
                        MessageBox.Show("Фурнитура добавлена!");
                    }
            }
            catch
            {
                MessageBox.Show("Произошла ошибка!");
            }
        }
        private void PostTkani(object sender, RoutedEventArgs e)
        {
            try
            {
                if (textilId.Text != "" || textilName.Text != "" || textilWidth.Text != "" || textilLenght.Text != "" || textilCost.Text != "" || textilCount.Text != "" || textilPicture.Text != "")

                    using (prak1Entities1 prak = new prak1Entities1())
                    {
                        foreach (Picture picture in prak.Picture)
                        {
                            if (picture.Picture1 == textilPicture.Text)
                            {
                                textilePicture = picture.Picture_Id;
                            }    
                        };

                        Textile textile = new Textile()
                        {
                            Textile_Id = textilId.Text,
                            Name = textilName.Text,
                            Lenght = Convert.ToInt32(textilLenght.Text),
                            Width = Convert.ToInt32(textilWidth.Text),
                            Cost = Convert.ToInt32(textilCost.Text),
                            Picture_Id = textilePicture
                        };
                        prak.Textile.Add(textile);


                        Fabric_Textile fabric = new Fabric_Textile()
                        {
                            Textile_Id = textilId.Text,
                            Roll = Convert.ToInt32(textilCount.Text),
                            Lenght = Convert.ToInt32(textilLenght.Text)
                        };
                        prak.Fabric_Textile.Add(fabric);

                        CreateWordDocument1();

                        prak.SaveChanges();
                        MessageBox.Show("Ткань добавлена!");
                    }
            }
            catch(Exception ex)
            {
                Clipboard.SetText(ex.ToString());
                MessageBox.Show(ex.ToString());
            }
        }

        private void CreateWordDocument()
        {
            Application wordApp = new Application();

            // Создание нового документа
            Document wordDoc = wordApp.Documents.Add();

            // Создание таблицы в документе
            Table wordTable = wordDoc.Tables.Add(
                wordDoc.Paragraphs[1].Range, // Добавляем таблицу в начало документа
                1, // Количество строк
                6, // Количество столбцов
                WdDefaultTableBehavior.wdWord9TableBehavior, // Поведение таблицы по умолчанию
                WdAutoFitBehavior.wdAutoFitWindow); // Подгонка размеров таблицы под содержимое

            // Заголовки столбцов таблицы
            wordTable.Cell(1, 1).Range.Text = "артикул";
            wordTable.Cell(1, 2).Range.Text = "название";
            wordTable.Cell(1, 3).Range.Text = "длина";
            wordTable.Cell(1, 4).Range.Text = "ширина";
            wordTable.Cell(1, 5).Range.Text = "цена";
            wordTable.Cell(1, 6).Range.Text = "количество";

            // Добавление данных из текстовых полей в таблицу
            wordTable.Rows.Add(); // Добавляем новую строку в таблицу
            wordTable.Cell(2, 1).Range.Text = fyrnId.Text;
            wordTable.Cell(2, 2).Range.Text = fyrnName.Text;
            wordTable.Cell(2, 3).Range.Text = fyrnLenght.Text;
            wordTable.Cell(2, 4).Range.Text = fyrnWidth.Text;
            wordTable.Cell(2, 5).Range.Text = fyrnCost.Text;
            wordTable.Cell(2, 6).Range.Text = fyrnCount.Text;

            // Отображение диалогового окна сохранения файла
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Документ Word (*.docx)|*.docx|Все файлы (*.*)|*.*";
            if (saveFileDialog.ShowDialog() == true)
            {
                // Сохранение документа
                wordDoc.SaveAs2(Path.GetFullPath(saveFileDialog.FileName));
            }

            // Закрытие документа и приложения Word
            wordDoc.Close();
            wordApp.Quit();
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

                var cellValue = cells[0];
                var cellText = cells[1];
                var cellValueText = cells[3];
                var cellWidth = cells[4];
                var cellCost = cells[5];

                AboutFyr product = new AboutFyr();
                product.Show();
                product.plow.Text = cellValue;
                product.name.Text = cellText;
                product.width.Text = cellWidth;
                product.height.Text = cellValueText;
                product.cost.Text = cellCost;

                var s = cellValue;
                Image image = new Image();
                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.UriSource = new Uri($"FyrImage/{s}.JPG", UriKind.Relative);
                bitmap.EndInit();
                product.image.Source = bitmap;
            }
        }

        private void CreateWordDocument1()
        {
            Application wordApp = new Application();

            // Создание нового документа
            Document wordDoc = wordApp.Documents.Add();

            // Создание таблицы в документе
            Table wordTable = wordDoc.Tables.Add(
                wordDoc.Paragraphs[1].Range, // Добавляем таблицу в начало документа
                1, // Количество строк
                6, // Количество столбцов
                WdDefaultTableBehavior.wdWord9TableBehavior, // Поведение таблицы по умолчанию
                WdAutoFitBehavior.wdAutoFitWindow); // Подгонка размеров таблицы под содержимое

            // Заголовки столбцов таблицы
            wordTable.Cell(1, 1).Range.Text = "артикул";
            wordTable.Cell(1, 2).Range.Text = "название";
            wordTable.Cell(1, 3).Range.Text = "длина";
            wordTable.Cell(1, 4).Range.Text = "ширина";
            wordTable.Cell(1, 5).Range.Text = "цена";
            wordTable.Cell(1, 6).Range.Text = "количество";

            // Добавление данных из текстовых полей в таблицу
            wordTable.Rows.Add(); // Добавляем новую строку в таблицу
            wordTable.Cell(2, 1).Range.Text = textilId.Text;
            wordTable.Cell(2, 2).Range.Text = textilName.Text;
            wordTable.Cell(2, 3).Range.Text = textilLenght.Text;
            wordTable.Cell(2, 4).Range.Text = textilWidth.Text;
            wordTable.Cell(2, 5).Range.Text = textilCost.Text;
            wordTable.Cell(2, 6).Range.Text = textilCount.Text;

            // Отображение диалогового окна сохранения файла
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Документ Word (*.docx)|*.docx|Все файлы (*.*)|*.*";
            if (saveFileDialog.ShowDialog() == true)
            {
                // Сохранение документа
                wordDoc.SaveAs2(Path.GetFullPath(saveFileDialog.FileName));
            }

            // Закрытие документа и приложения Word
            wordDoc.Close();
            wordApp.Quit();
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
                var cellValueText = cells[6];
                var cellWidth = cells[5];
                var cellCost = cells[7];

                AboutTextile product = new AboutTextile();
                product.Show();
                product.plow.Text += cellValue;
                product.name.Text += cellText;
                product.width.Text += cellWidth;
                product.height.Text += cellValueText;
                product.cost.Text += cellCost;

                var s = cellValue;
                Image image = new Image();
                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.UriSource = new Uri($"TextileImage/{s}.JPG", UriKind.Relative);
                bitmap.EndInit();
                product.image.Source = bitmap;
            }
        }
    }
}
