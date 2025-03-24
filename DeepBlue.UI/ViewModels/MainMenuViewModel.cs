using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Windows.Input;
using DeepBlue.Models;
using DeepBlue.Models.ZHL_16;
using DeepBlue.Views.FormulaWindows;
using static DeepBlue.GlobalEnums;

namespace DeepBlue.ViewModels
{
    public partial class MainMenuViewModel : ObservableObject
    {
        [ObservableProperty]
        private string greeting;

        [ObservableProperty]
        private string decompressionPlan;
        public ObservableCollection<Compartment> Compartments { get; } = new ObservableCollection<Compartment>();
        
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

        public ObservableCollection<DiveLevel> DiveLevels { get; } = new ObservableCollection<DiveLevel>();

        public MainMenuViewModel()
        {
            Greeting = "Welcome to DeepBlue.UI";
            List<Gas> gasList = new List<Gas>();
            
            gasList.Add(new Gas("18/45", 0.45, 0.18, false, 232));
            gasList.Add(new Gas("50%", 0, 0.50, false, 232));
            
            DiveLevels.Add(new DiveLevel(50, 25, gasList[0], false));
            
            Zhl16 zhl16 = new Zhl16(20, 80, gasList, DiveLevels.ToList(), 1, 0.0567);
            zhl16.Initialize();
            zhl16.CalculateDive();

            foreach (var level in zhl16.DiveLevels)
            {
                DiveLevels.Add(level);
            }
            
            foreach (var compartment in zhl16.Compartments)
            {
                Compartments.Add(compartment);
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