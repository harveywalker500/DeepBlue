using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DeepBlue
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public UnitTypes unit;

        public enum UnitTypes
        {
            Meters,
            Feet,
            None
        }

        public MainWindow()
        {
            unit = UnitTypes.Meters;
            try
            {
                DisclaimerHandler.showDisclaimer();
                InitializeComponent();
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
                this.unit = UnitTypes.Meters;
                ATMDepth_ATM.Text = "";
                ATMDepth_Depth.Text = "";
                Debug.WriteLine("Meters selected.");
            }
            else
            {
                this.unit = UnitTypes.Feet;
                ATMDepth_ATM.Text = "";
                ATMDepth_Depth.Text = "";
                Debug.WriteLine("Feet selected.");
            }
        }

        private void CalculateATM_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (unit == UnitTypes.Meters)
                {
                    double conversion = MetricFormulas.DepthToATA(Convert.ToDouble(ATMDepth_Depth.Text));
                    ATMDepth_ATM.Text = Math.Round(conversion, 2).ToString();
                }
                else if (unit == UnitTypes.Feet)
                {
                    double conversion = ImperialFormulas.DepthToATA(Convert.ToDouble(ATMDepth_Depth.Text));
                    ATMDepth_ATM.Text = Math.Round(conversion, 2).ToString();
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
                    ATMDepth_Depth.Text = Math.Round(conversion, 2).ToString();
                }
                else if (unit == UnitTypes.Feet)
                {
                    double conversion = ImperialFormulas.ATAToDepth(Convert.ToDouble(ATMDepth_ATM.Text));
                    ATMDepth_Depth.Text = Math.Round(conversion, 2).ToString();
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
                MetresFeet_Metres.Text = Math.Round(conversion, 2).ToString();
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
                MetresFeet_Feet.Text = Math.Round(conversion, 2).ToString();
            }
            catch
            {
                MessageBox.Show("Please enter a valid number.");
            }
        }
        

        private void ShowDisclaimer_Click(object sender, RoutedEventArgs e)
        {
            DisclaimerHandler.showDisclaimer();
        }

        private void CalculatePO2_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                double conversion = CommonFormulas.Po2(Convert.ToDouble(FO2.Text), Convert.ToDouble(P.Text));
                PO2.Text = (Math.Round(conversion, 2) / 100).ToString();
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
                FO2.Text = Math.Round(conversion, 2).ToString();
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
                P.Text = Math.Round(conversion, 2).ToString();
            }
            catch
            {
                MessageBox.Show("Please enter a valid number.");
            }
        }
    }
}
