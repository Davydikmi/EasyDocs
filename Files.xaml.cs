using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using static System.Net.WebRequestMethods;

namespace EasyDocs
{
    /// <summary>
    /// Логика взаимодействия для Files.xaml
    /// </summary>
    public partial class Files : Page
    {

        public ObservableCollection<SourceFiles> listview_items {  get; set; }
        public Files()
        {
            InitializeComponent();
            listview_items = new ObservableCollection<SourceFiles>
            {
                new SourceFiles { filename = "Document1.txt" },
                new SourceFiles { filename = "Presentation.pptx" },
                new SourceFiles { filename = "Image.png" }
            };
            fileListView.ItemsSource = listview_items;
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            if (listview_items == null)
            {
                MessageBox.Show("Коллекция не инициализирована");
                return;
            }

            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Filter = "Документы (*.doc; *.docx; *.txt;)|*.doc;*.docx;*.txt";
            bool? success = fileDialog.ShowDialog();
            if (success == true)
            {
                SourceFiles sourceFiles = new SourceFiles();
                sourceFiles.filename = fileDialog.SafeFileName;
                sourceFiles.filepath = fileDialog.FileName;
                listview_items.Add(new SourceFiles { filename = sourceFiles.filename });
                sourceFiles.AddFile();
            }
        }
        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            SourceFiles sourceFiles = new SourceFiles();
            var selectedItem = fileListView.SelectedItem as SourceFiles;

            if (selectedItem != null)
            {
                MessageBoxResult confirmation = MessageBox.Show($"Вы точно хотите удалить «{selectedItem.filename}» ?", "Подтверждение", MessageBoxButton.OKCancel, MessageBoxImage.Question);
                if (confirmation == MessageBoxResult.OK)
                {
                    sourceFiles.filepath = selectedItem.filepath;
                    sourceFiles.filename = selectedItem.filename;
                    if (sourceFiles.DeleteFile())
                    {
                        listview_items.Remove(selectedItem);
                        MessageBox.Show($"Файл «{selectedItem.filename}» был успешно удален", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                    }

                }


            }

            else MessageBox.Show("Для удаления элемента необходимо его сначала выделить!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
        }
        private void Reset_Click(object sender, RoutedEventArgs e) 
        { 

        }
    }
    public class FileItem
    {
        public string FileName { get; set; }
    }

}
