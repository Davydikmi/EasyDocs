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

        public void AddClient()
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

        public void DeleteClient()
        {
            List<ClientData> clients = new List<ClientData>();
            if (File.Exists(filepath))
            {
                string existingData = File.ReadAllText(filepath);
                clients = JsonConvert.DeserializeObject<List<ClientData>>(existingData) ?? new List<ClientData>();

                var clientToRemove = clients.FirstOrDefault(client => client.id_numb == id_numb);
                if (clientToRemove != null)
                {
                    // Удаляем клиента из списка
                    clients.Remove(clientToRemove);

                    // Сохранение обновлённый список обратно в JSON-файл
                    string updatedJsonData = JsonConvert.SerializeObject(clients, Formatting.Indented);
                    File.WriteAllText(filepath, updatedJsonData);

                    MessageBox.Show("Клиент успешно удалён.", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("Клиент с указанным идентификационным номером не найден.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }

            }
            else MessageBox.Show("Файл с данными не найден.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);

        }

        public void ChangeClient(ClientData inputClientData)
        {
            if (!File.Exists(filepath))
            {
                Console.WriteLine("Файл с данными клиентов не найден.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            string jsonData = File.ReadAllText(filepath);
            List<ClientData> clients = JsonConvert.DeserializeObject<List<ClientData>>(jsonData) ?? new List<ClientData>();

            // Поиск клиента по идентификационному номеру (можно выбрать другой уникальный критерий)
            var existingClient = clients.FirstOrDefault(c => c.id_numb == inputClientData.id_numb);

            if (existingClient != null)
            {
                existingClient.FIO = inputClientData.FIO;
                existingClient.phone_numb = inputClientData.phone_numb;
                existingClient.birth_date = inputClientData.birth_date;
                existingClient.adress = inputClientData.adress;
                existingClient.passport_SeriesNumb = inputClientData.passport_SeriesNumb;
            }
            else
            {
                Console.WriteLine("Клиент с заданным идентификационным номером не найден.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            string updatedData = JsonConvert.SerializeObject(clients, Formatting.Indented);
            File.WriteAllText(filepath, updatedData);
        }

    }
}