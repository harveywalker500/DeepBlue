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
        public UnitTypes Unit { get; set; }
        public bool Rounding { get; set; }
        public WaterTypes Water { get; set; }

    }
}
