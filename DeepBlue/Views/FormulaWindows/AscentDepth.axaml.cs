using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using DeepBlue.ViewModels;

namespace DeepBlue.Views.FormulaWindows
{
    public partial class AscentDepth : Window
    {
        public AscentDepth()
        {
            InitializeComponent();
            DataContext = new AscentDepthViewModel();
        }
    }
}