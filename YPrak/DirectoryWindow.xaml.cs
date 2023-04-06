using System;
using System.IO;
using System.IO.Packaging;
using System.Linq;
using System.Windows;
using System.Xaml;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Xps.Packaging;
using System.Windows.Xps.Serialization;
using System.Windows.Markup;
using XamlWriter = System.Windows.Markup.XamlWriter;
using System.Windows.Documents;
using System.Text;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using DocumentFormat.OpenXml;
using System.Data;
using Table = DocumentFormat.OpenXml.Wordprocessing.Table;
using TableRow = DocumentFormat.OpenXml.Wordprocessing.TableRow;
using TableCell = DocumentFormat.OpenXml.Wordprocessing.TableCell;
using Paragraph = DocumentFormat.OpenXml.Wordprocessing.Paragraph;
using Run = DocumentFormat.OpenXml.Wordprocessing.Run;
using Microsoft.Win32;

namespace YPrak
{
    /// <summary>
    /// Логика взаимодействия для DirectoryWindow.xaml
    /// </summary>
    public partial class DirectoryWindow : Window
    {
        public DirectoryWindow()
        {
            InitializeComponent(); 
            UpdateOstFyr();
            UpdateOstTkani();
            UpdateSpTkani();
            UpdateSpFyr();
            Edin.SelectedIndex = 1;
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
                             where Fabric_Fyr.Count > 0
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

        public void UpdateIz(double edin)
        {
            datagridItem.ItemsSource = null;
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
                datagridItem.ItemsSource = query1.ToList();
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

        private void UpdateOstTkani()
        {
            using (prak1Entities1 prak = new prak1Entities1())
            {
                var query1 = from Textile in prak.Textile
                             join Fabric_Textile in prak.Fabric_Textile on Textile.Textile_Id equals Fabric_Textile.Textile_Id
                             where Fabric_Textile.Roll > 0
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
                var query = from Textile in prak.Textile
                            join Fabric_Textile in prak.Fabric_Textile on Textile.Textile_Id equals Fabric_Textile.Textile_Id
                            select new
                            {
                                Артикул = Textile.Textile_Id,
                                Название = Textile.Name,
                                Рулон = Fabric_Textile.Roll,
                                Длина = Fabric_Textile.Lenght
                            };
                datagrid3.ItemsSource = query.ToList();

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

        private void PrintDoc(DataGrid dataGrid, string header)
        {
            string text = "";
            foreach (var a in dataGrid.Items)
            {
                text += a + "\n";
            }

            /// Печать отчета
            
            PrintDialog printDlg = new PrintDialog();
            TextBlock visual = new TextBlock();
            visual.Inlines.Add(text);
            visual.Margin = new Thickness(5);
            visual.TextWrapping = TextWrapping.Wrap;
            Size pageSize = new Size(printDlg.PrintableAreaWidth, printDlg.PrintableAreaHeight);
            visual.Measure(pageSize);
            visual.Arrange(new Rect(0, 0, pageSize.Width, pageSize.Height));
            if (printDlg.ShowDialog() == true)
                printDlg.PrintVisual(visual, header);
        }

        private void Search1_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (Search1.Text != "")
                {
                    string s = Search1.Text;

                    datagridItem.Items.Refresh();
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
                        datagridItem.ItemsSource = query1.ToList();
                        Search1.Text = "";
                    }
                }
                else
                {
                    MessageBox.Show("Изделий не найдено!");
                }
            }
        }

        private void Button_Click_6(object sender, RoutedEventArgs e)
        {
            using (WordprocessingDocument doc = WordprocessingDocument.Create("output.txt", WordprocessingDocumentType.Document))
            {
                /// Создаем основной раздел (MainDocumentPart)
                MainDocumentPart mainPart = doc.AddMainDocumentPart();
                mainPart.Document = new Document();

                /// Создаем пустой тело документа
                Body body = new Body();
                mainPart.Document.Append(body);

                /// Создаем таблицу
                Table table = new Table();


                /// Заголовки столбцов
                TableRow headerRow = new TableRow();
                foreach (DataGridColumn column in datagrid1.Columns)
                {
                    headerRow.Append(new TableCell(new Paragraph(new Run(column.Header.ToString()))));
                }
                table.Append(headerRow);

                /// Данные таблицы
                foreach (var row in datagrid1.Items)
                {
                    TableRow tableRow = new TableRow();
                    foreach (DataGridColumn column in datagrid1.Columns)
                    {
                        string cellValue = row.GetType().GetProperty(column.SortMemberPath).GetValue(row, null)?.ToString() ?? "";
                        tableRow.Append(new TableCell(new Paragraph(new Run(cellValue))));
                    }
                    table.Append(tableRow);
                }

                /// Добавляем таблицу в тело документа
                body.Append(table);
            }
        }

        private void Button_Click_7(object sender, RoutedEventArgs e)
        {
            PrintDoc(datagrid2, header2.Text);
        }

        private void Button_Click_8(object sender, RoutedEventArgs e)
        {
            PrintDoc(datagrid4, header4.Text);
        }

        private void Button_Click_9(object sender, RoutedEventArgs e)
        {
            PrintDoc(datagrid3, header3.Text);
        }

        private void Button_Click_10(object sender, RoutedEventArgs e)
        {
            PrintDoc(datagrid1, header1.Text);
        }
    }
}
