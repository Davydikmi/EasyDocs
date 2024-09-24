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

namespace EasyDocs
{
    /// <summary>
    /// Логика взаимодействия для Files.xaml
    /// </summary>
    public partial class Files : Page
    {

        public ObservableCollection<SourceFiles> file_items {  get; set; }
        public Files()
        {
            InitializeComponent();
            file_items = new ObservableCollection<SourceFiles>
            {
                new SourceFiles { filename = "Document1.txt" },
                new SourceFiles { filename = "Presentation.pptx" },
                new SourceFiles { filename = "Image.png" }
            };
            fileListView.ItemsSource = file_items;
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            if (file_items == null)
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
                file_items.Add(new SourceFiles { filename = sourceFiles.filename });

                string projectDirectory = Directory.GetCurrentDirectory();
                string targetDirectory = System.IO.Path.Combine(projectDirectory, "source_files"); 

                // Проверяем, существует ли папка "ProjectFiles", если нет - создаем её
                if (!Directory.Exists(targetDirectory))
                {
                    Directory.CreateDirectory(targetDirectory);
                }

                // Определяем целевой путь, куда скопировать файл
                string targetFilePath = System.IO.Path.Combine(targetDirectory, sourceFiles.filename);
                File.Copy(sourceFiles.filepath, targetFilePath, overwrite: true);
            }
        }
        private void Delete_Click(object sender, RoutedEventArgs e)
        {
        
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
