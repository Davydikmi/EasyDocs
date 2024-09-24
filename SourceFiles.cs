﻿using System;
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
       
        public void AddFile()
        {
            string projectDirectory = Directory.GetCurrentDirectory();
            string targetDirectory = System.IO.Path.Combine(projectDirectory, folder_name);

            // Проверка на существование папки
            if (!Directory.Exists(targetDirectory)) Directory.CreateDirectory(targetDirectory);

            // Определение целевого пути, куда будет копироваться файл
            string targetFilePath = System.IO.Path.Combine(targetDirectory, filename);
            // Копирование файла
            File.Copy(filepath, targetFilePath, overwrite: true);
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

        }
    }
}