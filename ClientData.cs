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
using System.Data.SqlClient;

namespace EasyDocs
{
    public class ClientData
    {
        [JsonProperty("FIO")]
        [NotEqualTo("ФИО")]
        [LettersOnly("ФИО")]
        [MaxLength(50, ErrorMessage = "Максимальная длина ФИО должна быть не более 50 символов.")]
        [MinLength(3, ErrorMessage = "Миниимальная длина ФИО должна быть не менее 3х символов.")]
        public string FIO { get; set; }

        [JsonProperty("phone_number")]
        [NotEqualTo("Номер телефона")]
        [MaxLength(25, ErrorMessage = "Максимальная длина номера телефона должна быть не более 25 символов")]
        [MinLength(3, ErrorMessage = "Минимальная длина номера телефона должна быть не более 3х символов")]
        public string phone_numb { get; set; }

        [JsonProperty("birthDate")]
        [NotEqualTo("Дата рождения")]
        [ValidDate(ErrorMessage = "Поле \"Дата рождения\" должно содержать существующую дату.")]
        public string birth_date { get; set; }

        [JsonProperty("adress")]
        [NotEqualTo("Адрес")]
        [MaxLength(200, ErrorMessage = "Максимальная длина адреса должна быть не более 200 символов.")]
        [MinLength(5, ErrorMessage = "Минимальная длина адреса должна быть не менее 5 символов.")]

        public string adress { get; set; }

        [JsonProperty("passportSeriesNumb")]
        [NotEqualTo("Серия и номер паспорта")]
        [MaxLength(40, ErrorMessage = "Максимальная длина серии и номера паспорта должна быть не более 40 символов.")]
        [MinLength(3, ErrorMessage = "Минимальная длина серии и номера паспорта должна быть не менее 3х символов.")]
        public string passport_SeriesNumb { get; set; }

        [JsonProperty("id_number")]
        [NotEqualTo("Идентификационный номер")]
        [StringLength(14, ErrorMessage = "Длина идентификационного номера строго 14 символов.")]
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
            MessageBox.Show("Клиент успешно добавлен.", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
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
                    clients.Remove(clientToRemove);

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

        public void UpdateClient(ClientData inputClientData)
        {
            if (!File.Exists(filepath))
            {
                Console.WriteLine("Файл с данными клиентов не найден.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            string jsonData = File.ReadAllText(filepath);
            List<ClientData> clients = JsonConvert.DeserializeObject<List<ClientData>>(jsonData) ?? new List<ClientData>();

            // Поиск нужного клиента по идентификационному номеру
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
            MessageBox.Show("Данные клиента успешно изменены.", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        public bool CheckDuplicateID()
        {
            int counter = 0; 
            if (!File.Exists(filepath)) return true;
            string jsonData = File.ReadAllText(filepath);
            List<ClientData> clients = JsonConvert.DeserializeObject<List<ClientData>>(jsonData) ?? new List<ClientData>();
            foreach (ClientData client in clients) 
            {
                if(client.id_numb==id_numb) counter++;
            }
            if(counter == 1) return true;
            else return false;
        }

    }
}