using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.IO;

namespace EasyDocs
{
    public class TextReplacer
    {
        [JsonProperty("FIO_marker")]
        [NotEqualTo("Метка для ФИО")]
        [MaxLength(20, ErrorMessage = "Максимальная длинна метки для ФИО должна быть не более 20 символов.")]
        [MinLength(3,ErrorMessage = "Минимальная длинна метки для ФИО должна быть не менее 3х символов.")]

        // упорядочить так для каждого для удобного чтения
       // допустимые символы (A-Z,a-z,А-Я,а-я) никаких чисел и спец символов
        public string FIO_marker { get; set; }

        [JsonProperty("phone_number_marker"), MaxLength(25, ErrorMessage = "Максимальная длинна номера телефона должна быть не более 25 символов"), NotEqualTo("Метка для номера телефона")]
        public string phone_numb_marker { get; set; }

        [JsonProperty("birthDate_marker"), NotEqualTo("Метка для даты рождения")]
        public string birth_date_marker { get; set; }

        [JsonProperty("adress_marker"), MaxLength(200, ErrorMessage = "Максимальная длинна адреса должна быть не более 200 символов."), NotEqualTo("Метка для адреса")]
        public string adress_marker { get; set; }

        [JsonProperty("passportSeriesNumb_marker"), MaxLength(40, ErrorMessage = "Максимальная длина серии и номера паспорта должна быть не более 40 символов."), NotEqualTo("Метка для серии и номера паспорта")]
        public string passport_SeriesNumb_marker { get; set; }

        [JsonProperty("id_number_marker"), NotEqualTo("Метка для идентификационного номера")]
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

            string updatedData = JsonConvert.SerializeObject(newMarkers, Formatting.Indented);
            File.WriteAllText(filepath, updatedData);
            MessageBox.Show("Метки успешно обновлены","Информация",MessageBoxButton.OK, MessageBoxImage.Information);
        }

    }
}
