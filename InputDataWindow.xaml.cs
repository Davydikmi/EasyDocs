using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
using System.Globalization;


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

            var validationContext = new ValidationContext(clientData);
            var validationResults = new List<System.ComponentModel.DataAnnotations.ValidationResult>();
            if(!Validator.TryValidateObject(clientData, validationContext, validationResults, true))
            {
                StringBuilder errorMessages = new StringBuilder();
                foreach (var error in validationResults)
                {
                    MessageBox.Show(error.ErrorMessage);
                }
                MessageBox.Show(errorMessages.ToString(), "Ошибка валидации", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            else
            {
                clientData.AddClientToJson();
                this.Close();
            }


            
        }
    }
}
