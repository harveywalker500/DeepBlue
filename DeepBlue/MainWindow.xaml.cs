using System.IO;
using System.Windows;
using System.Windows.Controls;
using DeepBlue.FormulaWindows;

namespace DeepBlue
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public UnitTypes Unit { get; set; }
        public bool Rounding { get; set; }
        public WaterTypes Water { get; set; }

        public MainWindow()
        {
            try
            {
                DisclaimerHandler.ShowDisclaimer();
                InitializeComponent();

                // Initial state of settings
                this.Unit = UnitTypes.Meters;
                UnitsCBMetric.IsSelected = true;

                this.Rounding = true;
                RoundingOn.IsChecked = true;

                this.Water = WaterTypes.Salt;
                WaterTypeSalt.IsSelected = true;
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
                this.Unit = UnitTypes.Meters;
            }
            else
            {
                this.Unit = UnitTypes.Feet;
            }
        }

        private void ShowDisclaimer_Click(object sender, RoutedEventArgs e)
        {
            DisclaimerHandler.ShowDisclaimer();
        }

        private void RoundingOn_Checked(object sender, RoutedEventArgs e)
        {
            this.Rounding = true;
        }

        private void RoundingOff_Checked(object sender, RoutedEventArgs e)
        {
            this.Rounding = false;
        }

        private void AtmDepthMenuItem_OnClick(object sender, RoutedEventArgs e)
        {
            AtmDepth atmDepth = new AtmDepth(Unit, Water, Rounding);
            atmDepth.Show();
        }

        private void MetresFeetMenuItem_OnClick(object sender, RoutedEventArgs e)
        {
            MetresFeet metresFeet = new MetresFeet(Unit, Water, Rounding);
            metresFeet.Show();
        }

        private void PO2TriangleMenuItem_OnClick(object sender, RoutedEventArgs e)
        {
            PO2Triangle po2Triangle = new PO2Triangle(Unit, Water, Rounding);
            po2Triangle.Show();
        }

        private void GasReserveMenuItem_OnClick(object sender, RoutedEventArgs e)
        {
            GasReserve gasReserve = new GasReserve(Unit, Water, Rounding);
            gasReserve.Show();
        }

        private void AscentDepthMenuItem_OnClick(object sender, RoutedEventArgs e)
        {
            AscentDepth ascentDepth = new AscentDepth(Unit, Water, Rounding);
            ascentDepth.Show();
        }

        private void AscentTimeMenuItem_OnClick(object sender, RoutedEventArgs e)
        {
            AscentTime ascentTime = new AscentTime(Unit, Water, Rounding);
            ascentTime.Show();
        }

        private void OTUsMenuItem_OnClick(object sender, RoutedEventArgs e)
        {
            OTUs otus = new OTUs(Unit, Water, Rounding);
            otus.Show();
        }

        private void EADMenuItem_Click(object sender, RoutedEventArgs e)
        {
            EAD ead = new EAD(Unit, Water, Rounding);
            ead.Show();
        }

        private void MaxDepthMenuItem_Click(object sender, RoutedEventArgs e)
        {
            MaxDepth maxDepth = new MaxDepth(Unit, Water, Rounding);
            maxDepth.Show();
        }

        private void SACRateMenuItem_Click(object sender, RoutedEventArgs e)
        {
            if (Unit == UnitTypes.Meters)
            {
                SACRateMetric SACRate = new SACRateMetric(Unit, Water, Rounding);
                SACRate.Show();
            }
            else if (Unit == UnitTypes.Feet)
            {
                SACRateImperial SACRate = new SACRateImperial(Unit, Water, Rounding);
                SACRate.Show();
            }
            else
            {
                MessageBox.Show("Error! Unit is not set. Please double check and try again.");
            }
        }

        private void GasRequirementEstimateMenuItem_Click(object sender, RoutedEventArgs e)
        {
            GasRequirementEstimate gasRequirementEstimate = new GasRequirementEstimate(Unit, Water, Rounding);
            gasRequirementEstimate.Show();
        }
    }
}
