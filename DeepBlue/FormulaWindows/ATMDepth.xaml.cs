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

namespace DeepBlue
{
    /// <summary>
    /// Interaction logic for ATMDepth.xaml
    /// </summary>
    public partial class AtmDepth : Window
    {
        private UnitTypes _unit;
        private WaterTypes _water;
        private bool _rounding;

        public AtmDepth(UnitTypes unit, WaterTypes water, bool rounding)
        {
            InitializeComponent();

            this._unit = unit;
            this._water = water;
            this._rounding = rounding;
        }

        private void CalculateATM_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (_unit == UnitTypes.Meters)
                {
                    double conversion = MetricFormulas.DepthToAta(Convert.ToDouble(ATMDepth_Depth.Text));
                    if (_rounding)
                    {
                        ATMDepth_ATM.Text = Math.Round(conversion, 2).ToString();
                    }
                    else
                    {
                        ATMDepth_ATM.Text = conversion.ToString();
                    }
                }
                else if (_unit == UnitTypes.Feet)
                {
                    double conversion = ImperialFormulas.DepthToAta(Convert.ToDouble(ATMDepth_Depth.Text));
                    if (_rounding)
                    {
                        ATMDepth_ATM.Text = Math.Round(conversion, 2).ToString();
                    }
                    else
                    {
                        ATMDepth_ATM.Text = conversion.ToString();
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

        private void CalculateDepth_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (_unit == UnitTypes.Meters)
                {
                    double conversion = MetricFormulas.AtaToDepth(Convert.ToDouble(ATMDepth_ATM.Text));
                    if (_rounding)
                    {
                        ATMDepth_Depth.Text = Math.Round(conversion, 2).ToString();
                    }
                    else
                    {
                        ATMDepth_Depth.Text = conversion.ToString();
                    }
                }
                else if (_unit == UnitTypes.Feet)
                {
                    double conversion = ImperialFormulas.AtaToDepth(Convert.ToDouble(ATMDepth_ATM.Text));
                    if (_rounding)
                    {
                        ATMDepth_Depth.Text = Math.Round(conversion, 2).ToString();
                    }
                    else
                    {
                        ATMDepth_Depth.Text = conversion.ToString();
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
