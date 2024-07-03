using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    /// Interaction logic for SACRateMetric.xaml
    /// </summary>
    public partial class SACRateImperial : Window
    {
        UnitTypes unit;
        WaterTypes water;
        bool rounding;
        public SACRateImperial(UnitTypes _unit, WaterTypes _water, bool _rounding)
        {
            this.unit = _unit;
            this.water = _water;
            this.rounding = _rounding;
            InitializeComponent();
        }

        private void SACRateCalculate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                double cylinder_size;
                if (DoublesCheckbox.IsEnabled)
                {
                    cylinder_size = Convert.ToDouble(SACRate_CylinderSize.Text) * 2;
                } else
                {
                    cylinder_size = Convert.ToDouble(SACRate_CylinderSize.Text);
                }

                double calculation = ImperialFormulas.SacRate(Convert.ToDouble(SACRate_PSI.Text),
                                                              Convert.ToDouble(SACRate_WorkingPressure.Text),
                                                              cylinder_size,
                                                              Convert.ToDouble(SACRate_Depth.Text),
                                                              Convert.ToDouble(SACRate_Time.Text));

                if (rounding)
                {
                    SACRateResult.Text = $"SAC Rate: {Math.Round(calculation, 2)} cu ft/min.";
                } else
                {
                    SACRateResult.Text = $"SAC Rate: {calculation} cu ft/min.";
                }
            }
            catch
            {
                MessageBox.Show("Please enter a valid number.");
            }
        }
    }
}
