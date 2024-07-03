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
    /// Interaction logic for MetresFeet.xaml
    /// </summary>
    public partial class MetresFeet : Window
    {
        private UnitTypes _unit;
        private WaterTypes _water;
        private bool _rounding;

        public MetresFeet(UnitTypes unit, WaterTypes water, bool rounding)
        {
            InitializeComponent();

            this._unit = unit;
            this._water = water;
            this._rounding = rounding;
        }

        private void CalculateMetres_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                double conversion = CommonFormulas.FeetToMetres(Convert.ToDouble(MetresFeet_Feet.Text));
                if (_rounding)
                {
                    MetresFeet_Metres.Text = Math.Round(conversion, 2).ToString();
                }
                else
                {
                    MetresFeet_Metres.Text = conversion.ToString();
                }
            }
            catch
            {
                MessageBox.Show("Please enter a valid number.");
            }
        }

        private void CalculateFeet_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                double conversion = CommonFormulas.MetresToFeet(Convert.ToDouble(MetresFeet_Metres.Text));
                if (_rounding)
                {
                    MetresFeet_Feet.Text = Math.Round(conversion, 2).ToString();
                }
                else
                {
                    MetresFeet_Feet.Text = conversion.ToString();
                }
            }
            catch
            {
                MessageBox.Show("Please enter a valid number.");
            }
        }
    }
}
