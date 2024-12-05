﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
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
using Microsoft.Win32;


namespace EasyDocs
{
    /// <summary>
    /// Логика взаимодействия для MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void PageLoaded(object sender, RoutedEventArgs e)
        {

            if (File.Exists(ClientData.filepath))
            {
                try
                {
                    string jsonData = File.ReadAllText(ClientData.filepath);

                    List<ClientData> clients = JsonConvert.DeserializeObject<List<ClientData>>(jsonData);

                    if (clients != null)
                    {
                        var sortedClients = clients.OrderBy(c => c.FIO).ToList();
                        foreach (var client in sortedClients)
                        {
                            ClientComboBox.Items.Add(client.FIO);
                        }
                    }
                    else return;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }

            else
            {
                StreamWriter writer = new StreamWriter(ClientData.filepath);
                writer.Close();
            }
                

            SourceFiles sourceFiles = new SourceFiles();
            if (Directory.Exists(sourceFiles.targetDirectory))
            {
                string[] files = Directory.GetFiles(sourceFiles.targetDirectory);
                foreach (string file in files)
                {
                    FileComboBox.Items.Add(System.IO.Path.GetFileName(file));
                }
            }
            else
            {
                Directory.CreateDirectory(SourceFiles.folder_name);
            }
        }

        private void SelectedClientCB(object sender, SelectionChangedEventArgs e)
        {
            if (File.Exists(ClientData.filepath))
            {
                string jsonData = File.ReadAllText(ClientData.filepath);
                List<ClientData> clients = JsonConvert.DeserializeObject<List<ClientData>>(jsonData) ?? new List<ClientData>();
                var existingClient = clients.FirstOrDefault(c => c.FIO == ClientComboBox.SelectedItem.ToString());

                if (existingClient != null)
                {
                    FIOTextBox.Text = existingClient.FIO;
                    PhoneNumberTextBox.Text = existingClient.phone_numb;
                    BirthDateTextBox.Text = existingClient.birth_date;
                    AddressTextBox.Text = existingClient.adress;
                    PassportTextBox.Text = existingClient.passport_SeriesNumb;
                    IDNumberTextBox.Text = existingClient.id_numb;
                }
                else
                {
                    Console.WriteLine("Клиент с заданным ФИО не найден.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }
            else 
            { 
                MessageBox.Show("Файл с данными клиентов не обнаружен.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
        }

        private void FillButton_Click(object sender, RoutedEventArgs e)
        {

            if (ClientComboBox.SelectedIndex == -1)
            {
                MessageBox.Show("Клиент не выбран.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error); 
                return;
            }
            else if(FileComboBox.SelectedIndex == -1)
            {
                MessageBox.Show("Шаблон для заполнения не выбран.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            else 
            {
                ClientData clientData = new ClientData();
                clientData.FIO = FIOTextBox.Text;
                clientData.adress = AddressTextBox.Text;
                clientData.phone_numb = PhoneNumberTextBox.Text;
                clientData.birth_date = BirthDateTextBox.Text;
                clientData.id_numb = IDNumberTextBox.Text;
                clientData.passport_SeriesNumb = PassportTextBox.Text;

                string templateFilename = FileComboBox.Text;
                string filledFilename = $"{clientData.FIO.Split()[0]}_{templateFilename}";

                // Загрузка меток из файла
                string jsonData = File.ReadAllText(TextReplacer.filepath, Encoding.UTF8);
                TextReplacer markers = JsonConvert.DeserializeObject<TextReplacer>(jsonData);

                Dictionary<string, string> map = markers.MarkersMap(clientData);
                

                switch (System.IO.Path.GetExtension(templateFilename))
                {
                    case ".doc":
                        markers.FillDoc(templateFilename,filledFilename, map);
                        break;
                    case ".docx":
                        markers.FillDocX(templateFilename, filledFilename, map);
                        break;
                    default:
                        MessageBox.Show("Недопустимое расширение файла.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                }
            }
        }
    }
}
