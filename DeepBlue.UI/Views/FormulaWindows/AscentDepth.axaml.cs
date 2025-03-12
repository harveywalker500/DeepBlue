using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using DeepBlue.ViewModels;
using static DeepBlue.GlobalEnums;

namespace DeepBlue.Views.FormulaWindows
{
    public partial class AscentDepth : Window
    {
        public AscentDepth(UnitTypes unit, WaterTypes waterType, bool rounding)
        {
            InitializeComponent();
            DataContext = new AscentDepthViewModel(unit, waterType, rounding);
        }
    }
}