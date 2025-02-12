using Avalonia.Controls;
using DeepBlue.ViewModels;

namespace DeepBlue.Views
{
    public partial class MainMenu : Window
    {
        public MainMenu()
        {
            InitializeComponent();
            DataContext = new MainMenuViewModel();
        }
    }
}