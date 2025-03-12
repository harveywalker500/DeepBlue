using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Windows.Input;
using static DeepBlue.GlobalEnums;

namespace DeepBlue.ViewModels
{
    public partial class AscentDepthViewModel : ObservableObject
    {
        [ObservableProperty]
        private string _bottomDepth;

        [ObservableProperty]
        private string _firstStopDepth;

        [ObservableProperty]
        private string _ascentDepthResult;

        private readonly UnitTypes _unit;
        private readonly WaterTypes _waterType;
        private readonly bool _rounding;

        public ICommand CalculateAscentDepthCommand { get; }

        public AscentDepthViewModel(UnitTypes unit, WaterTypes waterType, bool rounding)
        {
            _unit = unit;
            _waterType = waterType;
            _rounding = rounding;
            CalculateAscentDepthCommand = new RelayCommand(CalculateAscentDepth);
        }

        public AscentDepthViewModel()
        {
            CalculateAscentDepthCommand = new RelayCommand(CalculateAscentDepth);
        }

        private void CalculateAscentDepth()
        {
            try
            {
                double conversion = CommonFormulas.AscentDepth(Convert.ToDouble(BottomDepth), Convert.ToDouble(FirstStopDepth));
                if (_rounding)
                {
                    if (_unit == UnitTypes.Metres)
                    {
                        AscentDepthResult = $"Ascent Depth: {Math.Round(conversion, 2)} meters.";
                    }
                    else if (_unit == UnitTypes.Feet)
                    {
                        AscentDepthResult = $"Ascent Depth: {Math.Round(conversion, 2)} feet.";
                    }
                }
                else
                {
                    if (_unit == UnitTypes.Metres)
                    {
                        AscentDepthResult = $"Ascent Depth: {conversion} meters.";
                    }
                    else if (_unit == UnitTypes.Feet)
                    {
                        AscentDepthResult = $"Ascent Depth: {conversion} feet.";
                    }
                }
            }
            catch
            {
                AscentDepthResult = "Please enter a valid number.";
            }
        }
    }
}