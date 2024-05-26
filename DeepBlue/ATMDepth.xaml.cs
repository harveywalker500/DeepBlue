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
using System.Windows.Navigation;
using System.Windows.Shapes;
using static DeepBlue.MainWindow;

namespace DeepBlue.FormulaPages
{
    /// <summary>
    /// Interaction logic for ATMDepth.xaml
    /// </summary>
    public partial class ATMDepth : Page
    {
        public ATMDepth()
        {
            InitializeComponent();
        }

        private void CalculateATM_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (unit == UnitTypes.Meters)
                {
                    double conversion = MetricFormulas.DepthToATA(Convert.ToDouble(ATMDepth_Depth.Text));
                    if (rounding)
                    {
                        ATMDepth_ATM.Text = Math.Round(conversion, 2).ToString();
                    }
                    else
                    {
                        ATMDepth_ATM.Text = conversion.ToString();
                    }
                }
                else if (unit == UnitTypes.Feet)
                {
                    double conversion = ImperialFormulas.DepthToATA(Convert.ToDouble(ATMDepth_Depth.Text));
                    if (rounding)
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
                if (unit == UnitTypes.Meters)
                {
                    double conversion = MetricFormulas.ATAToDepth(Convert.ToDouble(ATMDepth_ATM.Text));
                    if (rounding)
                    {
                        ATMDepth_Depth.Text = Math.Round(conversion, 2).ToString();
                    }
                    else
                    {
                        ATMDepth_Depth.Text = conversion.ToString();
                    }
                }
                else if (unit == UnitTypes.Feet)
                {
                    double conversion = ImperialFormulas.ATAToDepth(Convert.ToDouble(ATMDepth_ATM.Text));
                    if (rounding)
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
