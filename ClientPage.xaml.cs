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

namespace EasyDocs
{
    /// <summary>
    /// Логика взаимодействия для ClientPage.xaml
    /// </summary>
    public partial class ClientPage : Page
    {
            public string FullName { get; set; }
    public string BirthDate { get; set; }
    public int Id { get; set; }
        public ObservableCollection<ClientData> listview_items { get; set; }

        public ClientPage()
        {
            InitializeComponent();
            listview_items = new ObservableCollection<ClientData>();
            DataListView.ItemsSource = listview_items;
            UpdateListView();
        }

        private void Add_Click(object sender, RoutedEventArgs e) 
        {
            InputDataWindow inputDataWindow = new InputDataWindow();
            inputDataWindow.ShowDialog();
            UpdateListView();
        }
        private void Change_Click(object sender, RoutedEventArgs e)
        {
            ChangeDataWindow changeDataWindow = new ChangeDataWindow();
            changeDataWindow.ShowDialog();
        }
        private void Delete_Click(object sender, RoutedEventArgs e) { }
        private void Reset_Click(object sender, RoutedEventArgs e) { }
        private void UpdatePage(object sender, RoutedEventArgs e)
        {
            UpdateListView();
        }

        public void UpdateListView()
        {

            listview_items.Clear();

            if (File.Exists(ClientData.filepath))
            {

                    string jsonData = File.ReadAllText(ClientData.filepath);
                    List<ClientData> clients = JsonConvert.DeserializeObject<List<ClientData>>(jsonData);


                    if (clients != null)
                    {
                        foreach (var client in clients)
                        {

                            listview_items.Add(new ClientData
                            {
                                FIO = client.FIO,
                                birth_date = client.birth_date,
                                id_numb = client.id_numb
                            });
                        }
                    }


            }
        }
    }
}
