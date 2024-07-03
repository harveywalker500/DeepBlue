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
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class AscentTime : Window
    {
        private UnitTypes _unit;
        private WaterTypes _waterType;
        private bool _rounding;
        public AscentTime(UnitTypes unit, WaterTypes waterType, bool rounding)
        {
            InitializeComponent();
            _unit = unit;
            _waterType = waterType;
            _rounding = rounding;
        }

        private void CalculateAscentTime_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                double conversion = CommonFormulas.AscentTime(Convert.ToDouble(AscentTime_BottomDepth.Text), Convert.ToDouble(AscentTime_StopDepth.Text), Convert.ToDouble(AscentTime_AscentRate.Text));
                if (_rounding)
                {
                    AscentTimeResult.Text = $"Ascent Time: {Math.Round(conversion, 2).ToString()} minutes.";
                }
                else
                {
                    AscentTimeResult.Text = $"Ascent Time: {conversion.ToString()} minutes.";
                }
            }
            catch
            {
                MessageBox.Show("Please enter a valid number.");
            }
        }
    }
}
