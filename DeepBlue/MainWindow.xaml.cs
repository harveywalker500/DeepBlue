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
                string disclaimerText = ReadFile("C:\\Users\\harve\\source\\repos\\DeepBlue\\DeepBlue\\disclaimer.txt");
                MessageBox.Show(disclaimerText, "Disclaimer", MessageBoxButton.OK, MessageBoxImage.Warning);
                InitializeComponent();
            } catch (FileNotFoundException)
            {
                MessageBox.Show("Disclaimer file not found. Please ensure that the file 'disclaimer.txt' is in the same directory as the executable.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            } catch (Exception)
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
        public static string ReadFile(string filePath)
        {
            try
            {
                if (File.Exists(filePath))
                {
                    string FileContent = File.ReadAllText(filePath);
                    Console.WriteLine($"File '{filePath}' read successfully.");
                    return FileContent;
                }
                else
                {
                    throw new FileNotFoundException($"File '{filePath}' not found.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error reading file '{filePath}': {ex.Message}");
            }
        }

        private void ShowDisclaimer_Click(object sender, RoutedEventArgs e)
        {
            string disclaimerText = ReadFile("C:\\Users\\harve\\source\\repos\\DeepBlue\\DeepBlue\\disclaimer.txt");
            MessageBox.Show(disclaimerText, "Disclaimer", MessageBoxButton.OK, MessageBoxImage.Warning);
        }
    }
}
