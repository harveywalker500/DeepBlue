using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Windows.Input;

namespace DeepBlue.ViewModels
{
    public partial class MainMenuViewModel : ObservableObject
    {
        [ObservableProperty]
        private string greeting;

        public ICommand AtmDepthCommand { get; }
        public ICommand MetresFeetCommand { get; }
        public ICommand PO2TriangleCommand { get; }
        public ICommand GasReserveCommand { get; }
        public ICommand AscentDepthCommand { get; }
        public ICommand AscentTimeCommand { get; }
        public ICommand OTUsCommand { get; }
        public ICommand EADCommand { get; }
        public ICommand MaxDepthCommand { get; }
        public ICommand SACRateCommand { get; }
        public ICommand GasRequirementEstimateCommand { get; }
        public ICommand TurnPressureCommand { get; }
        public ICommand ShowDisclaimerCommand { get; }

        public MainMenuViewModel()
        {
            Greeting = "Welcome to DeepBlue";

            AtmDepthCommand = new RelayCommand(OnAtmDepth);
            MetresFeetCommand = new RelayCommand(OnMetresFeet);
            PO2TriangleCommand = new RelayCommand(OnPO2Triangle);
            GasReserveCommand = new RelayCommand(OnGasReserve);
            AscentDepthCommand = new RelayCommand(OnAscentDepth);
            AscentTimeCommand = new RelayCommand(OnAscentTime);
            OTUsCommand = new RelayCommand(OnOTUs);
            EADCommand = new RelayCommand(OnEAD);
            MaxDepthCommand = new RelayCommand(OnMaxDepth);
            SACRateCommand = new RelayCommand(OnSACRate);
            GasRequirementEstimateCommand = new RelayCommand(OnGasRequirementEstimate);
            TurnPressureCommand = new RelayCommand(OnTurnPressure);
            ShowDisclaimerCommand = new RelayCommand(OnShowDisclaimer);
        }

        private void OnAtmDepth() { /* Implement logic here */ }
        private void OnMetresFeet() { /* Implement logic here */ }
        private void OnPO2Triangle() { /* Implement logic here */ }
        private void OnGasReserve() { /* Implement logic here */ }
        private void OnAscentDepth() { /* Implement logic here */ }
        private void OnAscentTime() { /* Implement logic here */ }
        private void OnOTUs() { /* Implement logic here */ }
        private void OnEAD() { /* Implement logic here */ }
        private void OnMaxDepth() { /* Implement logic here */ }
        private void OnSACRate() { /* Implement logic here */ }
        private void OnGasRequirementEstimate() { /* Implement logic here */ }
        private void OnTurnPressure() { /* Implement logic here */ }
        private void OnShowDisclaimer() { /* Implement logic here */ }
    }
}