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
    /// Interaction logic for GasRequirementEstimate.xaml
    /// </summary>
    public partial class GasRequirementEstimate : Window
    {
        UnitTypes unit;
        WaterTypes water;
        bool rounding;
        public GasRequirementEstimate(UnitTypes _unit, WaterTypes _water, bool _rounding)
        {
            InitializeComponent();
            unit = _unit;
            water = _water;
            rounding = _rounding;
        }

        private void GasRequirementEstimateCalculate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (unit == UnitTypes.Meters)
                {
                    double calculation = MetricFormulas.GasRequirementEstimate(Convert.ToDouble(GasRequirementEstimate_Time.Text),
                                                                               Convert.ToDouble(GasRequirementEstimate_SAC.Text),
                                                                               Convert.ToDouble(GasRequirementEstimate_Depth.Text));

                    if (rounding)
                    {
                        GasRequirementEstimateResult.Text = $"Gas Required: {Math.Round(calculation, 2)} litres.";
                    }
                    else
                    {
                        GasRequirementEstimateResult.Text = $"Gas Required: {calculation} litres.";
                    }
                }
                else if (unit == UnitTypes.Feet)
                {
                    double calculation = ImperialFormulas.GasRequirementEstimate(Convert.ToDouble(GasRequirementEstimate_Time.Text),
                                                                               Convert.ToDouble(GasRequirementEstimate_SAC.Text),
                                                                               Convert.ToDouble(GasRequirementEstimate_Depth.Text));

                    if (rounding)
                    {
                        GasRequirementEstimateResult.Text = $"Gas Required: {Math.Round(calculation, 2)} cu ft.";
                    }
                    else
                    {
                        GasRequirementEstimateResult.Text = $"Gas Required: {calculation} cu ft.";
                    }
                } else
                {
                    MessageBox.Show("Please select a valid unit and try again.");
                }
            } catch
            {
                MessageBox.Show("Please enter a valid number.");
            }
        }
    }
}
