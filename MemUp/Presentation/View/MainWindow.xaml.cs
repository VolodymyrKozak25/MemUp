using Business_Logic_Level.ViewModel;
using Presentation.View;
using System.Windows;
using System.Windows.Controls;

namespace Presentation
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public readonly string enterUsernamePlaceholder;
        private readonly string searchCollectionPlaceholder;

        public MainWindow()
        {
            InitializeComponent();
            enterUsernamePlaceholder = "Enter username";
            searchCollectionPlaceholder = "Search collection";
        }

        private void TextBoxGotFocus(object sender, RoutedEventArgs e)
        {
            var textBox = sender as TextBox;

            if (textBox.Text == enterUsernamePlaceholder |
                textBox.Text == searchCollectionPlaceholder)
            {
                textBox.Text = "";
            }
        }

        private void TextBoxLostFocus(object sender, RoutedEventArgs e)
        {
            var textBox = sender as TextBox;

            if (string.IsNullOrWhiteSpace(textBox.Text))
            {
                if (textBox == enterUsernameTextbox)
                {
                    textBox.Text = enterUsernamePlaceholder;
                }
                else if (textBox == searchCollectionTextBox)
                {
                    textBox.Text = searchCollectionPlaceholder;
                }
                textBox.Opacity = 0.6;
            }
        }

        private void ShowCreateCollectionWindow(object sender, RoutedEventArgs e)
        {
            var dialogViewModel = new CreateCollectionWindowViewModel((MainWindowViewModel)mainWindowGrid.DataContext);
            var createCollectionWindow = new CreateCollectionWindow();
            createCollectionWindow.DataContext = dialogViewModel;

            createCollectionWindow.ShowDialog();
        }

        private void ShowEditCollectionWindow(object sender, RoutedEventArgs e)
        {
            var dialogViewModel = new EditCollectionWindowViewModel((MainWindowViewModel)mainWindowGrid.DataContext);
            var editCollectionWindow = new EditCollectionWindow();
            editCollectionWindow.DataContext = dialogViewModel;

            editCollectionWindow.ShowDialog();
        }

        private void ShowCreateMemWindow(object sender, RoutedEventArgs e)
        {
            var dialogViewModel = new CreateMemWindowViewModel((MainWindowViewModel)mainWindowGrid.DataContext);
            var createMemWindow = new CreateMemWindow();
            createMemWindow.DataContext = dialogViewModel;

            createMemWindow.ShowDialog();
        }

        private void ShowEditMemWindow(object sender, RoutedEventArgs e)
        {
            var dialogViewModel = new EditMemWindowViewModel((MainWindowViewModel)mainWindowGrid.DataContext);
            var editMemWindow = new EditMemWindow();
            editMemWindow.DataContext = dialogViewModel;

            editMemWindow.ShowDialog();
        }


    }
}
