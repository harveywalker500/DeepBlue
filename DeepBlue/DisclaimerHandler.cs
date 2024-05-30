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
            string disclaimerText = ReadFile("C:\\Users\\harve\\source\\repos\\DeepBlue\\DeepBlue\\disclaimer.txt");
            MessageBox.Show(disclaimerText, "Disclaimer", MessageBoxButton.OK, MessageBoxImage.Warning);
        }

        private static string ReadFile(string filePath)
        {
            try
            {
                if (File.Exists(filePath))
                {
                    string fileContent = File.ReadAllText(filePath);
                    Console.WriteLine($"File '{filePath}' read successfully.");
                    return fileContent;
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
    }
}
