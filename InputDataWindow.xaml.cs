using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace EasyDocs
{
    /// <summary>
    /// Логика взаимодействия для InputDataWindow.xaml
    /// </summary>
    public partial class InputDataWindow : Window
    {
        public InputDataWindow()
        {
            InitializeComponent();
        }

        //TextBox Placeholder 
        //- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        public void RemoveText(object sender, EventArgs e)
        {
            TextBox instance = (TextBox)sender;
            if (instance.Text == instance.Tag.ToString())
            { 
                instance.Text = "";
                instance.Foreground = System.Windows.Media.Brushes.Black;
            }
        }

        public void AddText(object sender, EventArgs e)
        {
            TextBox instance = (TextBox)sender;
            if (string.IsNullOrWhiteSpace(instance.Text))
            {
                instance.Text = instance.Tag.ToString();
                instance.Foreground = System.Windows.Media.Brushes.Gray;
            }
        }
        //- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            ClientData clientData = new ClientData();
            clientData.FIO = fioTextBox.Text;
            clientData.phone_numb = phoneNumberTextBox.Text;
            clientData.birth_date = birthDateTextBox.Text;
            clientData.adress = adressTextBox.Text;
            clientData.passport_SeriesNumb = passportTextBox.Text;
            clientData.id_numb = idNumberTextBox.Text;

            clientData.AddClientToJson(clientData);

            this.Close();
            
        }
    }
}
