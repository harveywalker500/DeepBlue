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
    /// Interaction logic for EAD.xaml
    /// </summary>
    public partial class EAD : Window
    {
        private UnitTypes unit;
        private WaterTypes water;
        private bool rounding;

        public EAD(UnitTypes unit, WaterTypes water, bool rounding)
        {
            InitializeComponent();
            this.unit = unit;
            this.water = water;
            this.rounding = rounding;
        }

        private void EADCalculate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (unit == UnitTypes.Meters)
                {
                    double calculation = MetricFormulas.Ead(Convert.ToDouble(EAD_O2Percentage.Text) / 100, Convert.ToDouble(EAD_Depth.Text));

                    if (rounding)
                    {
                        EADResult.Text = $"EAD: {Math.Round(calculation, 2)} meters.";
                    }
                    else
                    {
                        EADResult.Text = $"EAD: {calculation} meters.";
                    }
                }
                else if (unit == UnitTypes.Feet)
                {
                    double calculation = ImperialFormulas.Ead(Convert.ToDouble(EAD_O2Percentage.Text) / 100, Convert.ToDouble(EAD_Depth.Text));

                    if (rounding)
                    {
                        EADResult.Text = $"EAD: {Math.Round(calculation, 2)} feet.";
                    }
                    else
                    {
                        EADResult.Text = $"EAD: {calculation} feet.";
                    }
                } else
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
