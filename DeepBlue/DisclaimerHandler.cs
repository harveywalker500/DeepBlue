using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace DeepBlue
{
    public class DisclaimerHandler
    {
        public static void ShowDisclaimer()
        {
            // Change this to use a resource instead

            string disclaimerText = Properties.Resources.disclaimer;
            MessageBox.Show(disclaimerText, "Disclaimer", MessageBoxButton.OK, MessageBoxImage.Warning);
        }
    }
}
