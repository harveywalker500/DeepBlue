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
    /// Interaction logic for MaxDepth.xaml
    /// </summary>
    public partial class MaxDepth : Window
    {
        UnitTypes unit;
        WaterTypes water;
        bool rounding;
        public MaxDepth(UnitTypes _unit, WaterTypes _water, bool _rounding)
        {
            InitializeComponent();
            unit = _unit;
            water = _water;
            rounding = _rounding;
        }

        private void MaxDepthCalculate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (unit == UnitTypes.Meters)
                {
                    double calculation = MetricFormulas.MaxDepth(Convert.ToDouble(MaxDepth_O2Percentage.Text), Convert.ToDouble(MaxDepth_PO2.Text));

                    if (rounding)
                    {
                        MaxDepthResult.Text = $"Max Depth: {Math.Round(calculation, 2)} meters.";
                    }
                    else
                    {
                        MaxDepthResult.Text = $"Max Depth: {calculation} meters.";
                    }
                }
                else if (unit == UnitTypes.Feet)
                {
                    double calculation = ImperialFormulas.MaxDepth(Convert.ToDouble(MaxDepth_O2Percentage.Text), Convert.ToDouble(MaxDepth_PO2.Text));

                    if (rounding)
                    {
                        MaxDepthResult.Text = $"Max Depth: {Math.Round(calculation, 2)} feet.";
                    }
                    else
                    {
                        MaxDepthResult.Text = $"Max Depth: {calculation} feet.";
                    }
                } 
                else
                {
                    MessageBox.Show("Please select a unit type.");
                }
            }
            catch
            {
                MessageBox.Show("Please enter a valid number.");
            }
        }
    }
}
