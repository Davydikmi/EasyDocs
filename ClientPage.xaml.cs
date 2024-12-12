using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using System.ComponentModel.DataAnnotations;

namespace EasyDocs
{
    /// <summary>
    /// Логика взаимодействия для ClientPage.xaml
    /// </summary>
    public partial class ClientPage : Page
    {
        public ObservableCollection<ClientData> listview_items { get; set; }

        public ClientPage()
        {
            InitializeComponent();
            listview_items = new ObservableCollection<ClientData>();
            DataListView.ItemsSource = listview_items;
        }

        private void Add_Click(object sender, RoutedEventArgs e) 
        {
            InputDataWindow inputDataWindow = new InputDataWindow();
            inputDataWindow.ShowDialog();
            UpdateListView();
        }
        private void Change_Click(object sender, RoutedEventArgs e)
        {
            ClientData clientData = new ClientData();
            if (DataListView.SelectedItem == null)
            {
                MessageBox.Show("Что бы изменить элемент, сперва его необходимо выделить в таблице.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            var selectedClient = (ClientData)DataListView.SelectedItem;
            clientData.FIO = selectedClient.FIO;
            clientData.birth_date = selectedClient.birth_date;
            clientData.id_numb = selectedClient.id_numb;
            clientData.adress = selectedClient.adress;
            clientData.passport_SeriesNumb = selectedClient.passport_SeriesNumb;
            clientData.phone_numb = selectedClient.phone_numb;

            ChangeDataWindow changeDataWindow = new ChangeDataWindow(clientData);
            changeDataWindow.ShowDialog();
            UpdateListView();
        }
        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            ClientData clientData = new ClientData();
            if(DataListView.SelectedItem == null)
            {
                MessageBox.Show("Что бы удалить элемент, сперва его необходимо выделить в таблице.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            var selectedClient = (ClientData) DataListView.SelectedItem;
            clientData.FIO = selectedClient.FIO;
            clientData.birth_date = selectedClient.birth_date;
            clientData.id_numb = selectedClient.id_numb;
            clientData.adress = selectedClient.adress;
            clientData.passport_SeriesNumb = selectedClient.passport_SeriesNumb;
            clientData.phone_numb = selectedClient.phone_numb;

            MessageBoxResult result = MessageBox.Show($"Вы уверены, что хотите удалить данные «{clientData.FIO}» ?", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes) clientData.DeleteClient();
            UpdateListView();

        }
        private void Reset_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show($"Вы уверены, что хотите удалить данные всех клиентов ?", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes) 
            {
                listview_items.Clear();
                if (File.Exists(ClientData.filepath)) File.WriteAllText(ClientData.filepath, "[]");
                
                MessageBox.Show("Данные всех клиентов были успешно удалены.", "Оповещение", MessageBoxButton.OK, MessageBoxImage.Information);
                UpdateListView();

            }
        }
        private void UpdatePage(object sender, RoutedEventArgs e)  { UpdateListView(); }
        private void UpdateListView()
        {
            listview_items.Clear();

            if (File.Exists(ClientData.filepath))
            {
                try 
                {
                    string jsonData = File.ReadAllText(ClientData.filepath);

                    List<ClientData> clients = JsonConvert.DeserializeObject<List<ClientData>>(jsonData);
                    var sortedClients = clients.OrderBy(c => c.FIO).ToList();

                    foreach (var client in sortedClients)
                    {
                        var validationContext = new ValidationContext(client);
                        var validationResults = new List<System.ComponentModel.DataAnnotations.ValidationResult>();
                        if (!Validator.TryValidateObject(client, validationContext, validationResults, true))
                        {
                            foreach (var error in validationResults)
                            {
                                MessageBox.Show($"Ошибка у клиента {client.FIO}: {error.ErrorMessage}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                                Application.Current.MainWindow.Close();
                            }
                        }
                        else if (!client.CheckDuplicateID())
                        {
                            MessageBox.Show($"Ошибка у клиента {client.FIO}: Клиент с таким идентификационным номером уже существует.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                            Application.Current.MainWindow.Close();
                        }
                        // Если объект прошел валидацию, добавляем его в список
                        listview_items.Add(client);
                    }
                }
                catch(Exception ex) 
                {
                    MessageBox.Show(ex.Message,"Ошибка",MessageBoxButton.OK,MessageBoxImage.Error);
                }
            }
            else MessageBox.Show("Файл с данными клиентов не обнаружен.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);


        }
    }
}
