using System.Diagnostics;
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
            string disclaimerText = """
                This dive planning application is intended for use by certified divers only. By utilizing this tool, you acknowledge that you are a qualified diver with the necessary training and experience to plan and execute dives safely.
                The creator of this application hereby explicitly disclaims any liability for loss of equipment, injuries, or fatalities that may result from the use of this tool. While every effort has been made to provide accurate and reliable information, diving inherently carries risks, and users are responsible for assessing and mitigating these risks themselves.
                It is imperative to adhere to established diving protocols, exercise caution, and use sound judgment when utilizing this application. The information provided should be used as a reference tool and not as a substitute for proper dive training and certification.
                By using this application, you agree to release the creator from any and all liability associated with its use, and you accept full responsibility for your actions and the outcomes thereof while diving.

                Dive responsibly and stay safe!
                """;

            MessageBox.Show(disclaimerText, "Disclaimer", MessageBoxButton.OK, MessageBoxImage.Warning);
            InitializeComponent();
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
    }
}
