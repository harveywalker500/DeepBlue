using System.Configuration;
using System.Data;
using System.Windows;

namespace DeepBlue
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    
    public partial class App : Application
    {
        public UnitTypes unit { get; set; }
        public bool rounding { get; set; }
        public WaterTypes water { get; set; }

        public enum UnitTypes
        {
            Meters,
            Feet,
            None
        }

        public enum WaterTypes
        {
            Salt,
            Fresh,
            None
        }
    }
}
