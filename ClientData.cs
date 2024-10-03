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
        [JsonProperty("FIO"), MaxLength(50, ErrorMessage ="Максимальная длинна ФИО должна быть не более 50 символов."), NotEqualTo("ФИО")]
        public string FIO { get; set; }

        [JsonProperty("phone_number"), MaxLength(25,ErrorMessage = "Максимальная длинна номера телефона должна быть не более 25 символов"), NotEqualTo("Номер телефона")]
        public string phone_numb { get; set; }

        [JsonProperty("birthDate"), RegularExpression(@"^\d{2}\.\d{2}\.\d{4}$", ErrorMessage = "Дата должна быть в формате ДД.ММ.ГГГГ"), NotEqualTo("Дата рождения")]
        public string birth_date { get; set; }

        [JsonProperty("adress"), MaxLength(200, ErrorMessage = "Максимальная длинна адреса должна быть не более 200 символов."), NotEqualTo("Адрес")]
        public string adress { get; set; }

        [JsonProperty("passportSeriesNumb"), MaxLength(40, ErrorMessage = "Максимальная длина серии и номера паспорта должна быть не более 40 символов."), NotEqualTo("Серия и номер паспорта")]
        public string passport_SeriesNumb { get; set; }

        [JsonProperty("id_number"), StringLength(14, MinimumLength = 10, ErrorMessage = "Длинна идентификационного номера строго 14 символов."), NotEqualTo("Идентификационный номер")]
        public string id_numb { get; set; }

        public const string filepath = "ClientData.json";

        public void AddClientToJson()
        {
            List<ClientData> clients = new List<ClientData>();

            if (File.Exists(filepath))
            {
                string existingData = File.ReadAllText(filepath);
                clients = JsonConvert.DeserializeObject<List<ClientData>>(existingData) ?? new List<ClientData>();
            }

            else clients = new List<ClientData>();

            clients.Add(this);

            string updatedData = JsonConvert.SerializeObject(clients, Formatting.Indented);
            File.WriteAllText(filepath, updatedData);
        }
    }


    // Класс для атрибута NotEqualTo
    public class NotEqualToAttribute : ValidationAttribute
    {
        private readonly string _disallowedValue;

        public NotEqualToAttribute(string disallowedValue)
        {
            _disallowedValue = disallowedValue;
        }

        protected override System.ComponentModel.DataAnnotations.ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is string stringValue && stringValue == _disallowedValue)
            {
                return new System.ComponentModel.DataAnnotations.ValidationResult($"Поле \"{_disallowedValue}\" должно быть обязательно заполнено.");
            }
            return System.ComponentModel.DataAnnotations.ValidationResult.Success;
        }
    }
}