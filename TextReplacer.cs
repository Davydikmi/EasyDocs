using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.IO;
using System.Xml.Linq;
using System.IO.Packaging;
using Microsoft.Office.Interop.Word;
using Microsoft.Win32;

namespace EasyDocs
{
    public class TextReplacer
    {
        [JsonProperty("FIO_marker")]
        [NotEqualTo("Метка для ФИО")]
        [LettersOnly("Метка для ФИО")]
        [MaxLength(20, ErrorMessage = "Максимальная длина метки для ФИО должна быть не более 20 символов.")]
        [MinLength(3, ErrorMessage = "Минимальная длина метки для ФИО должна быть не менее 3х символов.")]
        public string FIO_marker { get; set; }

        [JsonProperty("phone_number_marker")]
        [NotEqualTo("Метка для номера телефона")]
        [LettersOnly("Метка для номера телефона")]
        [MaxLength(25, ErrorMessage = "Максимальная длина метки для номера телефона должна быть не более 25 символов")]
        [MinLength(3, ErrorMessage = "Минимальная длина метки для номера телефона должна быть не менее 3х символов")]
        public string phone_numb_marker { get; set; }

        [JsonProperty("birthDate_marker")]
        [NotEqualTo("Метка для даты рождения")]
        [LettersOnly("Метка для даты рождения")]
        [MaxLength(20, ErrorMessage = "Максимальная длина метки для даты рождения должна быть не более 20 символов")]
        [MinLength(3, ErrorMessage = "Минимальная длина метки для адреса должна быть не менее 3х символов")]
        public string birth_date_marker { get; set; }

        [JsonProperty("adress_marker")]
        [NotEqualTo("Метка для адреса")]
        [LettersOnly("Метка для адреса")]
        [MaxLength(20, ErrorMessage = "Максимальная длинна метки для адреса должна быть не более 20 символов.")]
        [MinLength(3, ErrorMessage = "Минимальная длинна метки для адреса должна быть не менее 3х символов.")]
        public string adress_marker { get; set; }

        [JsonProperty("passportSeriesNumb_marker")]
        [NotEqualTo("Метка для серии и номера паспорта")]
        [LettersOnly("Метка для серии и номера паспорта")]
        [MaxLength(20, ErrorMessage = "Максимальная длина серии и номера паспорта должна быть не более 40 символов.")]
        [MinLength(3, ErrorMessage = "Минимальная длина серии и номера паспорта должна быть не менее 3х символов.")]
        public string passport_SeriesNumb_marker { get; set; }

        [JsonProperty("id_number_marker")]
        [NotEqualTo("Метка для идентификационного номера")]
        [LettersOnly("Метка для идентификационного номера")]
        [MaxLength(20, ErrorMessage = "Максимальная длина идентификационного номера должна быть не более 40 символов.")]
        [MinLength(3, ErrorMessage = "Минимальная длина идентификационного номера должна быть не менее 3х символов.")]
        public string id_numb_marker { get; set; }

        [JsonProperty("bracket_type")]
        public string bracket_type { get; set; }

        public const string filepath = "Markers.json";

        public void updateMarks(TextReplacer newMarkers)
        {
            if (!File.Exists(filepath))
            {
                Console.WriteLine("Файл с метками не найден.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (newMarkers != null)
            {
                foreach (var property in newMarkers.GetType().GetProperties())
                {
                    if (property.PropertyType == typeof(string) && property.CanRead && property.CanWrite)
                    {
                        string currentValue = property.GetValue(newMarkers) as string;
                        if (currentValue != null)
                        {
                            property.SetValue(newMarkers, currentValue.Trim());
                        }
                    }
                }
            }

            string updatedData = JsonConvert.SerializeObject(newMarkers, Formatting.Indented);
            File.WriteAllText(filepath, updatedData);
            MessageBox.Show("Метки успешно обновлены", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        public void FillDoc(string templateFilename, string filledFilename, Dictionary<string, string> map)
        {
            string templateFilepath = Path.GetFullPath(Path.Combine(SourceFiles.folder_name, templateFilename));
            string filledFilepath = Path.GetFullPath(Path.Combine(SourceFiles.folder_name, filledFilename));

            if (!File.Exists(templateFilepath))
            {
                throw new FileNotFoundException("Файл шаблона не найден."); 
            }

            File.Copy(templateFilepath, filledFilepath, overwrite: true);
            // Создание объекта Word Application
            Microsoft.Office.Interop.Word.Application wordApp = new Microsoft.Office.Interop.Word.Application();
            Document doc = null;
            try
            {
                // Открытие копии шаблона в Word
                doc = wordApp.Documents.Open(filledFilepath);

                foreach (var pair in map)
                {
                    // Замена меток на
                    ReplaceDocFile(wordApp, pair.Key, pair.Value);
                }

                doc.Save();
            }
            finally
            {
                // Закрытие документа и приложения Word, освобождение ресурсов
                doc?.Close();
                wordApp.Quit();
            }

            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "All Files (*.*)|*.*", 
                Title = "Сохранить заполненный файл", 
                FileName = Path.GetFileName(filledFilename) 
            };

            if (saveFileDialog.ShowDialog() == true) 
            {
                string destinationPath = saveFileDialog.FileName;

                try
                {
                    if (File.Exists(destinationPath)) File.Delete(destinationPath);
                    File.Move(filledFilepath, destinationPath);
                    MessageBox.Show($"Файл успешно заполнен и сохранен по пути: {destinationPath}", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (IOException ex)
                {
                    throw new IOException($"Не удалось сохранить файл в указанное место: {ex.Message}");
                }
            }
            else File.Delete(filledFilepath);
            
        }

        private void ReplaceDocFile(Microsoft.Office.Interop.Word.Application wordApp, string findText, string replaceText)
        {
            //получение объекта Find для выполнения поиска и замены текста
            Find findObject = wordApp.Selection.Find;

            //очистка форматирования искомого текста
            findObject.ClearFormatting();
            findObject.Text = findText;

            //очистка форматирования текста для записи
            findObject.Replacement.ClearFormatting();
            findObject.Replacement.Text = replaceText;

            // замена всех вхождений искомого текста
            object replaceAll = WdReplace.wdReplaceAll;
            findObject.Execute(Replace: replaceAll);
        }

        public Dictionary<string, string> MarkersMap(ClientData client, string brackets)
        {
            if (string.IsNullOrWhiteSpace(brackets) || brackets.Length != 2)
                throw new ArgumentException("Скобки должны иметь только 2 символа.", nameof(brackets));

            char openBracket = brackets[0];
            char closeBracket = brackets[1];

            return new Dictionary<string, string>
            {
                { $"{openBracket}{FIO_marker}{closeBracket}", client.FIO },
                { $"{openBracket}{phone_numb_marker}{closeBracket}", client.phone_numb },
                { $"{openBracket}{adress_marker}{closeBracket}", client.adress },
                { $"{openBracket}{birth_date_marker}{closeBracket}", client.birth_date },
                { $"{openBracket}{passport_SeriesNumb_marker}{closeBracket}", client.passport_SeriesNumb },
                { $"{openBracket}{id_numb_marker}{closeBracket}", client.id_numb }
            };
        }
    }
}
