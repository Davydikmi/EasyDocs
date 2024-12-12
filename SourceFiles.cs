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
        public const string folder_name = "source_files";
        public string targetDirectory = Path.Combine(Directory.GetCurrentDirectory(), folder_name);

        public bool AddFile()
        {
            // Проверка на существование папки
            if (!Directory.Exists(targetDirectory)) Directory.CreateDirectory(targetDirectory);
            if (CheckDuplicateFileName())
            {
                string targetFilePath = System.IO.Path.Combine(targetDirectory, filename);

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
            if (Directory.Exists(targetDirectory))
            {
                string[] files = Directory.GetFiles(targetDirectory);

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
