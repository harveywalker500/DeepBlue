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
    /// Interaction logic for TurnPressure.xaml
    /// </summary>
    public partial class TurnPressureMetric : Window
    {
        UnitTypes unit;
        WaterTypes water;
        bool rounding;
        public TurnPressureMetric(UnitTypes _unit, WaterTypes _water, bool _rounding)
        {
            this.unit = _unit;
            this.water = _water;
            this.rounding = _rounding;

            InitializeComponent();
        }

        private void GasRequirementEstimateCalculate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                double calculation = MetricFormulas.TurnPressure(Convert.ToDouble(TurnPressure_StartingPressure.Text),
                                                                 Convert.ToDouble(TurnPressure_BottomVolume.Text),
                                                                 Convert.ToDouble(TurnPressure_CylinderCapacity.Text));

                if (rounding)
                {
                    TurnPressureResult.Text = $"Turn Pressure: {Math.Round(calculation, 2)} bar.";
                }
                else
                {
                    TurnPressureResult.Text = $"Turn Pressure: {calculation} bar.";
                }
            }
            catch
            {
                MessageBox.Show("Please enter a valid number.");
            }
        }
    }
}
