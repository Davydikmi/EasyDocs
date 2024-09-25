using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace EasyDocs
{
    public class SourceFiles
    {
        public string filename { get; set; }
        public string filepath { get; set; }
        private const string folder_name = "source_files";
       
        public bool AddFile()
        {
            string projectDirectory = Directory.GetCurrentDirectory();
            string targetDirectory = System.IO.Path.Combine(projectDirectory, folder_name);

            // Проверка на существование папки
            if (!Directory.Exists(targetDirectory)) Directory.CreateDirectory(targetDirectory);
            if (CheckDuplicateFileName())
            {
                // Определение целевого пути, куда будет копироваться файл
                string targetFilePath = System.IO.Path.Combine(targetDirectory, filename);
                // Копирование файла
                File.Copy(filepath, targetFilePath, overwrite: true);
                return true;
            }
            else return false;
        }

        public bool DeleteFile() 
        {

            if (File.Exists($@"{folder_name}/{filename}"))
            {
                File.Delete($@"{folder_name}/{filename}");
                return true;
            }
            else
            {
                MessageBox.Show("Файл не найден.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
        }
        
        public void ResetFiles()
        {
            string targetDirectory = Path.Combine(Directory.GetCurrentDirectory(), folder_name);
            if (Directory.Exists(targetDirectory))
            {
                // Получение всех файлов в папке
                string[] files = Directory.GetFiles(targetDirectory);

                // Удаление каждого файла
                foreach (string file in files)
                {
                    try
                    {
                        File.Delete(file);
                    }
                    catch (IOException ex)
                    {
                        MessageBox.Show($"Не удалось удалить файл: {file}. Ошибка: {ex.Message}");
                    }
                }

            }
            else MessageBox.Show("Папка с файлами не найдена.");
        }

        private bool CheckDuplicateFileName()
        {
            string targetDirectory = Path.Combine(Directory.GetCurrentDirectory(), folder_name);
            if (Directory.Exists(targetDirectory))
            {
                string[] files = Directory.GetFiles(targetDirectory);

                foreach (string file in files)
                {
                    if (Path.GetFileName(file).Equals(filename, StringComparison.OrdinalIgnoreCase))
                    {
                        MessageBox.Show("Файл с таким именем уже существует в папке программы!","Ошибка",MessageBoxButton.OK,MessageBoxImage.Error);
                        return false;
                    }
                }
                return true;
            }
            else
            {
                MessageBox.Show("Папка с файлами не найдена!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error); 
                return false;
            }

        }
    }
}
