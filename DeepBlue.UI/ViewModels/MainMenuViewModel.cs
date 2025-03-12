using System;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Windows.Input;
using DeepBlue.Models.ZHL_16;
using DeepBlue.Views.FormulaWindows;
using static DeepBlue.GlobalEnums;

namespace DeepBlue.ViewModels
{
    public partial class MainMenuViewModel : ObservableObject
    {
        [ObservableProperty]
        private string greeting;
        
        public UnitTypes Unit { get; set; }
        public WaterTypes WaterType { get; set; }
        public bool Rounding { get; set; }

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
            Greeting = "Welcome to DeepBlue.UI";
            
            Zhl16 Zhl16 = new Zhl16(20, 80);
            Console.WriteLine($"ZHL-16 successfully initialized.");
            foreach (var compartment in Zhl16.Compartments)  
            {
                Console.WriteLine(compartment);
            }

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

        private void OnAscentDepth()
        {
            var ascentDepthWindow = new AscentDepth(Unit, WaterType, Rounding);
            ascentDepthWindow.Show();
        }
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