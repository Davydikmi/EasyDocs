using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyDocs
{
    public class TextReplacer
    {
        [JsonProperty("FIO_marker"), MaxLength(50, ErrorMessage = "Максимальная длинна ФИО должна быть не более 50 символов."), NotEqualTo("Метка для ФИО")]
        public string FIO_marker { get; set; }

        [JsonProperty("phone_number_marker"), MaxLength(25, ErrorMessage = "Максимальная длинна номера телефона должна быть не более 25 символов"), NotEqualTo("Метка для номера телефона")]
        public string phone_numb_marker { get; set; }

        [JsonProperty("birthDate_marker"), RegularExpression(@"^\d{2}\.\d{2}\.\d{4}$", ErrorMessage = "Дата должна быть в формате ДД.ММ.ГГГГ"), NotEqualTo("Метка для даты рождения")]
        public string birth_date_marker { get; set; }

        [JsonProperty("adress_marker"), MaxLength(200, ErrorMessage = "Максимальная длинна адреса должна быть не более 200 символов."), NotEqualTo("Метка для адреса")]
        public string adress_marker { get; set; }

        [JsonProperty("passportSeriesNumb_marker"), MaxLength(40, ErrorMessage = "Максимальная длина серии и номера паспорта должна быть не более 40 символов."), NotEqualTo("Метка для серии и номера паспорта")]
        public string passport_SeriesNumb_marker { get; set; }

        [JsonProperty("id_number_marker"), StringLength(14, MinimumLength = 10, ErrorMessage = "Длинна идентификационного номера строго 14 символов."), NotEqualTo("Метка для идентификационного номера")]
        public string id_numb_marker { get; set; }

        [JsonProperty("bracket_type")]
        public string bracket_type { get; set; }

        public const string filepath = "Markers.json";

        

    }
}
