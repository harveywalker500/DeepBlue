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
    /// Interaction logic for OTUs.xaml
    /// </summary>
    public partial class OTUs : Window
    {
        private UnitTypes _unit;
        private WaterTypes _waterType;
        private bool _rounding;
        public OTUs(UnitTypes unit, WaterTypes waterType, bool rounding)
        {
            InitializeComponent();
            _unit = unit;
            _waterType = waterType;
            _rounding = rounding;
        }

        private void OTUsPerMinuteCalculate_OnClick(object sender, RoutedEventArgs e)
        {
            double calculation = CommonFormulas.OtuPerMinute(Convert.ToDouble(OTUsPerMinute_PO2.Text));
            if (_rounding)
            {
                OTUsPerMinuteResult.Text = $"OTUs per minute: {Math.Round(calculation, 2).ToString()} OTUs.";
            }
            else
            {
                OTUsPerMinuteResult.Text = $"OTUs per minute: {calculation.ToString()} OTUs.";
            }
        }

        private void OTUsTimeCalculate_OnClick(object sender, RoutedEventArgs e)
        {
            double calculation = CommonFormulas.TotalOtu(Convert.ToDouble(OTUsTime_OTUsPerMinute.Text), (Convert.ToDouble(OTUsTime_Time.Text)));

            if (_rounding)
            {
                OTUsTimeResult.Text = $"Total OTUs: {Math.Round(calculation, 2).ToString()} OTUs.";
            }
            else
            {
                OTUsTimeResult.Text = $"Total OTUs: {calculation.ToString()} OTUs.";
            }

        }
    }
}
