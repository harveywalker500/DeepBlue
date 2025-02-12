using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Windows.Input;

namespace DeepBlue.ViewModels
{
    public partial class AscentDepthViewModel : ObservableObject
    {
        [ObservableProperty]
        private string bottomDepth;

        [ObservableProperty]
        private string firstStopDepth;

        [ObservableProperty]
        private string ascentDepthResult;

        public ICommand CalculateAscentDepthCommand { get; }

        public AscentDepthViewModel()
        {
            CalculateAscentDepthCommand = new RelayCommand(CalculateAscentDepth);
        }

        private void CalculateAscentDepth()
        {
            // Implement the calculation logic here
            AscentDepthResult = "Calculated Ascent Depth";
        }
    }
}