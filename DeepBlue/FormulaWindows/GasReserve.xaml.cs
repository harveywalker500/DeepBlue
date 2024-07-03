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
    /// Interaction logic for GasReserve.xaml
    /// </summary>
    public partial class GasReserve : Window
    {
        private UnitTypes _unit;
        private WaterTypes _water;
        private bool _rounding;

        public GasReserve(UnitTypes unit, WaterTypes water, bool rounding)
        {
            InitializeComponent();
            _unit = unit;
            _water = water;
            _rounding = rounding;
        }

        private void CalculateGasReserve_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (_unit == UnitTypes.Meters)
                {
                    double calculation = CommonFormulas.GasReserve(Convert.ToDouble(GasReserve_Volume.Text), (Convert.ToDouble(GasReserve_Reserve.Text) / 100));
                    if (_rounding)
                    {
                        GasReserveResult.Text = $"Gas Reserve: {Math.Round(calculation, 2).ToString()} litres.";
                    }
                    else
                    {
                        GasReserveResult.Text = $"Gas Reserve: {calculation.ToString()} litres.";
                    }
                }
                else if (_unit == UnitTypes.Feet)
                {
                    double calculation = CommonFormulas.GasReserve(Convert.ToDouble(GasReserve_Volume.Text), (Convert.ToDouble(GasReserve_Reserve.Text) / 100));
                    if (_rounding)
                    {
                        GasReserveResult.Text = $"Gas Reserve: {Math.Round(calculation, 2).ToString()} cubic feet.";
                    }
                    else
                    {
                        GasReserveResult.Text = $"Gas Reserve: {calculation.ToString()} cubic feet.";
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
