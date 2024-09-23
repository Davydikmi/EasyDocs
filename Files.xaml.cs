using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace EasyDocs
{
    /// <summary>
    /// Логика взаимодействия для Files.xaml
    /// </summary>
    public partial class Files : Page
    {
        public Files()
        {
            InitializeComponent();
            ObservableCollection<FileItem> fileItems = new ObservableCollection<FileItem>
            {
                new FileItem { FileName = "Document1.txt" },
                new FileItem { FileName = "Presentation.pptx" },
                new FileItem { FileName = "Image.png" }
            };

            fileListView.ItemsSource = fileItems; // Привязываем данные к ListView

        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
        }
        private void Delete_Click(object sender, RoutedEventArgs e) { }
        private void Reset_Click(object sender, RoutedEventArgs e) { }
    }
    public class FileItem
    {
        public string FileName { get; set; }
    }

}
