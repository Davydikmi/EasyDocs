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

namespace EasyDocs
{
    /// <summary>
    /// Логика взаимодействия для ClientPage.xaml
    /// </summary>
    public partial class ClientPage : Page
    {
        public ObservableCollection<SourceFiles> listview_items { get; set; }
        public ClientPage()
        {
            InitializeComponent();
            listview_items = new ObservableCollection<SourceFiles>();
            fileListView.ItemsSource = listview_items;
        }

        private void Add_Click(object sender, RoutedEventArgs e) 
        {
            InputDataWindow inputDataWindow = new InputDataWindow();
            inputDataWindow.ShowDialog();
        }
        private void Change_Click(object sender, RoutedEventArgs e)
        {
            ChangeDataWindow changeDataWindow = new ChangeDataWindow();
            changeDataWindow.ShowDialog();
        }
        private void Delete_Click(object sender, RoutedEventArgs e) { }
        private void Reset_Click(object sender, RoutedEventArgs e) { }
    }
}
