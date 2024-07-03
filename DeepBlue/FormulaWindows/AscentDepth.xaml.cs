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

namespace DeepBlue.FormulaWindows
{
    /// <summary>
    /// Interaction logic for AscentDepth.xaml
    /// </summary>
    public partial class AscentDepth : Window
    {
        private UnitTypes _unit;
        private WaterTypes _waterType;
        private bool _rounding;
        public AscentDepth(UnitTypes unit, WaterTypes waterType, bool rounding)
        {
            InitializeComponent();
            _unit = unit;
            _waterType = waterType;
            _rounding = rounding;
        }

        private void CalculateAscentDepth_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                double conversion = CommonFormulas.AscentDepth(Convert.ToDouble(AscentDepth_BottomDepth.Text), Convert.ToDouble(AscentDepth_FirstStopDepth.Text));
                if (_rounding)
                {
                    if (_unit == UnitTypes.Meters)
                    {
                        AscentDepthResult.Text = $"Ascent Depth: {Math.Round(conversion, 2).ToString()} meters.";
                    } else if(_unit == UnitTypes.Feet)
                    {
                        AscentDepthResult.Text = $"Ascent Depth: {Math.Round(conversion, 2).ToString()} feet.";
                    }
                }
                else
                {
                    if (_unit == UnitTypes.Meters)
                    {
                        AscentDepthResult.Text = $"Ascent Depth: {conversion.ToString()} meters.";
                    }
                    else if (_unit == UnitTypes.Feet)
                    {
                        AscentDepthResult.Text = $"Ascent Depth: {conversion.ToString()} feet.";
                    }
                }
            }
            catch
            {
                MessageBox.Show("Please enter a valid number.");
            }
        }
    }
}
