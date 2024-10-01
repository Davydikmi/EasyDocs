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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace EasyDocs
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            MyFrame.Content = new MainPage();
            MainPageButton.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#4D7B94"));
            MainPageButton.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#4D7B94"));
            ChangeToUnpressedColor(ClientDataButton);
            ChangeToUnpressedColor(FilesButton);
            ChangeToUnpressedColor(InstructionButton);
        }
        private void MainButton_click(object sender, RoutedEventArgs e)
        {
            MyFrame.Content = new MainPage();
            MainPageButton.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#4D7B94"));
            MainPageButton.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#4D7B94"));
            ChangeToUnpressedColor(ClientDataButton);
            ChangeToUnpressedColor(FilesButton);
            ChangeToUnpressedColor(InstructionButton);
        }
        private void Tourists_click(object sender, RoutedEventArgs e)
        {
            MyFrame.Content = new ClientPage();
            ClientDataButton.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#4D7B94"));
            ClientDataButton.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#4D7B94"));
            ChangeToUnpressedColor(MainPageButton);
            ChangeToUnpressedColor(FilesButton);
            ChangeToUnpressedColor(InstructionButton);
        }

        private void Files_click(object sender, RoutedEventArgs e)
        {
            MyFrame.Content = new Files();
            FilesButton.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#4D7B94"));
            FilesButton.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#4D7B94"));
            ChangeToUnpressedColor(MainPageButton);
            ChangeToUnpressedColor(ClientDataButton);
            ChangeToUnpressedColor(InstructionButton);
        }

        private void Instruction_click(object sender, RoutedEventArgs e)
        {
            InstructionButton.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#4D7B94"));
            InstructionButton.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#4D7B94"));
            ChangeToUnpressedColor(MainPageButton);
            ChangeToUnpressedColor(ClientDataButton);
            ChangeToUnpressedColor(FilesButton);
        }

        private void ChangeToUnpressedColor(Button button) 
        {
            button.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#215B7A"));
            button.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#215B7A"));
        }


    }
}
