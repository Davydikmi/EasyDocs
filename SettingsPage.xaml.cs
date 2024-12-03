using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
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
    /// Логика взаимодействия для SettingsPage.xaml
    /// </summary>
    public partial class SettingsPage : Page
    {
        public SettingsPage()
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

        private void ChangeButton_Click(object sender, RoutedEventArgs e)
        {
            TextReplacer textReplacer = new TextReplacer();

            textReplacer.FIO_marker = FIOMarkerTextBox.Text;
            textReplacer.adress_marker = adressMarkerTextBox.Text;
            textReplacer.phone_numb_marker = phoneNumberMarkerTextBox.Text;
            textReplacer.birth_date_marker = birthDateMarkerTextBox.Text;
            textReplacer.passport_SeriesNumb_marker = passportMarkerTextBox.Text;
            textReplacer.id_numb_marker = idNumberMarkerTextBox.Text;

            if (square_brackets.IsChecked == true) textReplacer.bracket_type = "[]";
            if (brace.IsChecked == true) textReplacer.bracket_type = "{}";
            if (angle_brackets.IsChecked == true) textReplacer.bracket_type = "<>";
            if (slash_brackets.IsChecked == true) textReplacer.bracket_type = "//";

            var validationContext = new ValidationContext(textReplacer);
            var validationResults = new List<System.ComponentModel.DataAnnotations.ValidationResult>();

            if (!Validator.TryValidateObject(textReplacer, validationContext, validationResults, true))
            {
                foreach (var error in validationResults)
                {
                    MessageBox.Show(error.ErrorMessage, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }
            else textReplacer.updateMarks(textReplacer);
        }

        private void PageLoaded(object sender, RoutedEventArgs e)
        {
            if (File.Exists(TextReplacer.filepath))
            {
                try
                {
                    string jsonData = File.ReadAllText(TextReplacer.filepath, Encoding.UTF8);

                    TextReplacer markers = JsonConvert.DeserializeObject<TextReplacer>(jsonData);

                    if (markers != null)
                    {
                        FIOMarkerTextBox.Text = markers.FIO_marker;
                        phoneNumberMarkerTextBox.Text = markers.phone_numb_marker;
                        birthDateMarkerTextBox.Text = markers.birth_date_marker;
                        adressMarkerTextBox.Text = markers.adress_marker;
                        passportMarkerTextBox.Text = markers.passport_SeriesNumb_marker;
                        idNumberMarkerTextBox.Text = markers.id_numb_marker;

                        switch(markers.bracket_type)
                        {
                            case "[]": square_brackets.IsChecked = true; break;
                            case "{}": brace.IsChecked = true;           break;
                            case "//": slash_brackets.IsChecked = true;  break;
                            case "<>": angle_brackets.IsChecked = true;  break;
                            default:
                                MessageBox.Show("Несуществующие скобки","Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                                return;
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
                StreamWriter writer = new StreamWriter(TextReplacer.filepath);
                writer.Close();
            }
        }




    }
}
