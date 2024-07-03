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
    /// Interaction logic for PO2Triangle.xaml
    /// </summary>
    public partial class PO2Triangle : Window
    {
        private UnitTypes _unit;
        private WaterTypes _water;
        private bool _rounding;

        public PO2Triangle(UnitTypes unit, WaterTypes water, bool rounding)
        {
            InitializeComponent();

            this._unit = unit;
            this._water = water;
            this._rounding = rounding;
        }

        private void CalculatePO2_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                double conversion = CommonFormulas.Po2(Convert.ToDouble(FO2.Text), Convert.ToDouble(P.Text));
                if (_rounding)
                {
                    PO2.Text = (Math.Round(conversion, 2) / 100).ToString();
                }
                else
                {
                    PO2.Text = (conversion / 100).ToString();
                }
            }
            catch
            {
                MessageBox.Show("Please enter a valid number.");
            }
        }

        private void CalculateFO2_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                double conversion = CommonFormulas.Fo2(Convert.ToDouble(PO2.Text), Convert.ToDouble(P.Text)) * 100;
                if (_rounding)
                {
                    FO2.Text = Math.Round(conversion, 2).ToString();
                }
                else
                {
                    FO2.Text = conversion.ToString();
                }
            }
            catch
            {
                MessageBox.Show("Please enter a valid number.");
            }
        }

        private void CalculateP_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                double fo2Value = Convert.ToDouble(FO2.Text) / 100;
                double conversion = CommonFormulas.Pressure(fo2Value, Convert.ToDouble(P.Text));
                if (_rounding)
                {
                    P.Text = Math.Round(conversion, 2).ToString();
                }
                else
                {
                    P.Text = conversion.ToString();
                }
            }
            catch
            {
                MessageBox.Show("Please enter a valid number.");
            }
        }
    }
}
