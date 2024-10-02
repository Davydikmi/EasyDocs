using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Serialization;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices.ComTypes;
using System.Xml.Linq;

namespace EasyDocs
{
    public class ClientData
    {
        [JsonProperty("FIO"), Required(ErrorMessage ="Поле ФИО обязательно должно быть заполнено."), MaxLength(50, ErrorMessage ="Максимальная длинна ФИО должна быть не более 50 символов.")]
        public string FIO { get; set; }

        [JsonProperty("phone_number"), Required(ErrorMessage = "Поле номера телефона обязательно должно быть заполнено."),MaxLength(25,ErrorMessage = "Максимальная длинна номера телефона должна быть не более 25 символов")]
        public string phone_numb { get; set; }

        [JsonProperty("birthDate"),RegularExpression(@"^\d{2}\.\d{2}\.\d{4}$", ErrorMessage = "Дата должна быть в формате ДД.ММ.ГГГГ"), Required(ErrorMessage = "Поле даты рождения обязательно должно быть заполнено.")]
        public string birth_date { get; set; }

        [JsonProperty("passportSeriesNumb")] [MaxLength(40, ErrorMessage = "Максимальная длина серии и номера паспорта должна быть не более 40 символов.")] [Required(ErrorMessage = "Поле серии и номера паспорта обязательно должно быть заполнено.")]
        public string passport_SeriesNumb { get; set; }

        [JsonProperty("adress")] [MaxLength(200, ErrorMessage = "Максимальная длинна адреса должна быть не более 200 символов.")] [Required(ErrorMessage = "Поле адреса обязательно должно быть заполнено.")]
        public string adress { get; set; }

        [JsonProperty("id_number")] [StringLength(14, ErrorMessage = "Длинна идентификационного номера строго 14 символов.")] [Required(ErrorMessage = "Поле идентификационного номера обязательно должно быть заполнено.")]
        public string id_numb { get; set; }

        private const string filepath = "ClientData.json";

        public void AddClientToJson()
        {


        }
    }
}