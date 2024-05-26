using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace DeepBlue
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public App.UnitTypes unit;
        public bool rounding;
        public App.WaterTypes water;

        public MainWindow()
        {
            try
            {
                DisclaimerHandler.showDisclaimer();
                InitializeComponent();

                // Initial state of settings
                this.unit = App.UnitTypes.Meters;
                UnitsCBMetric.IsSelected = true;

                this.rounding = true;
                RoundingOn.IsChecked = true;

                this.water = App.WaterTypes.Salt;
                WaterTypeSalt.IsSelected = true;
            }
            catch (FileNotFoundException)
            {
                MessageBox.Show("Disclaimer file not found. Please ensure that the file 'disclaimer.txt' is in the same directory as the executable.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception)
            {
                MessageBox.Show("Error reading disclaimer file.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void UnitsComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (UnitsComboBox.SelectedIndex == 0)
            {
                this.unit = App.UnitTypes.Meters;
                ATMDepth_ATM.Text = "";
                ATMDepth_Depth.Text = "";
            }
            else
            {
                this.unit = App.UnitTypes.Feet;
                ATMDepth_ATM.Text = "";
                ATMDepth_Depth.Text = "";
            }
        }

        private void ShowDisclaimer_Click(object sender, RoutedEventArgs e)
        {
            DisclaimerHandler.showDisclaimer();
        }

        private void RoundingOn_Checked(object sender, RoutedEventArgs e)
        {
            this.rounding = true;
        }

        private void RoundingOff_Checked(object sender, RoutedEventArgs e)
        {
            this.rounding = false;
        }

        private void CalculateATM_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (unit == App.UnitTypes.Meters)
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
                else if (unit == App.UnitTypes.Feet)
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
                if (unit == App.UnitTypes.Meters)
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
                else if (unit == App.UnitTypes.Feet)
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

        private void CalculateMetres_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                double conversion = CommonFormulas.FeetToMetres(Convert.ToDouble(MetresFeet_Feet.Text));
                if (rounding)
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
                if (rounding)
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

        private void CalculatePO2_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                double conversion = CommonFormulas.Po2(Convert.ToDouble(FO2.Text), Convert.ToDouble(P.Text));
                if (rounding)
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
                if (rounding)
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
                double fo2value = Convert.ToDouble(FO2.Text) / 100;
                double conversion = CommonFormulas.Pressure(fo2value, Convert.ToDouble(P.Text));
                if (rounding)
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

        private void CalculateGasReserve_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (unit == App.UnitTypes.Meters)
                {
                    double calculation = CommonFormulas.GasReserve(Convert.ToDouble(GasReserve_Volume.Text), (Convert.ToDouble(GasReserve_Reserve.Text) / 100));
                    if (rounding)
                    {
                        GasReserveResult.Text = $"Gas Reserve: {Math.Round(calculation, 2).ToString()} litres.";
                    }
                    else
                    {
                        GasReserveResult.Text = $"Gas Reserve: {calculation.ToString()} litres.";
                    }
                }
                else if (unit == App.UnitTypes.Feet)
                {
                    double calculation = CommonFormulas.GasReserve(Convert.ToDouble(GasReserve_Volume.Text), (Convert.ToDouble(GasReserve_Reserve.Text) / 100));
                    if (rounding)
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
