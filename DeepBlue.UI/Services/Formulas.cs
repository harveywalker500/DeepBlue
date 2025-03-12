using System;

namespace DeepBlue
{
    public class CommonFormulas
    {
        /// <summary>
        /// Partial pressure of oxygen.
        /// </summary>
        /// <param name="fo2">Fraction of oxygen.</param>
        /// <param name="p">Pressure.</param>
        /// <returns>Partial pressure of oxygen.</returns>
        public static double Po2(double fo2, double p)
        {
            return fo2 * p;
        }

        /// <summary>
        /// Fraction of oxygen.
        /// </summary>
        /// <param name="po2">Partial pressure of oxygen.</param>
        /// <param name="p">Pressure.</param>
        /// <returns>Fraction of oxygen.</returns>
        public static double Fo2(double po2, double p)
        {
            return po2 / p;
        }

        /// <summary>
        /// Absolute pressure.
        /// </summary>
        /// <param name="fo2">Fraction of oxygen.</param>
        /// <param name="po2">Partial pressure of oxygen.</param>
        /// <returns>Absolute pressure.</returns>
        public static double Pressure(double fo2, double po2)
        {
            return po2 / fo2;
        }

        /// <summary>
        /// Gas reserve.
        /// </summary>
        /// <param name="volume">Volume of gas.</param>
        /// <param name="reserve">Gas reserve.</param>
        /// <returns>Gas reserve the desired gas.</returns>
        public static double GasReserve(double volume, double reserve)
        {
            return volume / (1 - reserve);
        }

        /// <summary>
        /// Ascent depth.
        /// </summary>
        /// <param name="bottom">Bottom depth.</param>
        /// <param name="stop">Stop depth.</param>
        /// <returns>Ascent depth.</returns>
        public static double AscentDepth(double bottom, double stop)
        {
            return ((bottom - stop) / 2) + stop;
        }

        /// <summary>
        /// Ascent time.
        /// </summary>
        /// <param name="bottom">Bottom depth.</param>
        /// <param name="stop">Stop depth.</param>
        /// <param name="rate">Rate of ascent.</param>
        /// <returns>Ascent time.</returns>
        public static double AscentTime(double bottom, double stop, double rate)
        {
            return (bottom - stop) / rate;
        }

        /// <summary>
        /// OTU per minute.
        /// </summary>
        /// <param name="po2">Partial pressure of oxygen.</param>
        /// <returns>OTU per minute.</returns>
        public static double OtuPerMinute(double po2)
        {
            if (po2 <= 0.5) // Avoiding mathematical issues if PO2 <= 0.5
                return 0;
            return Math.Pow(0.5 / (po2 - 0.5), -5.0 / 6.0);
        }

        /// <summary>
        /// Total OTU.
        /// </summary>
        /// <param name="otuMin">OTU per minute.</param>
        /// <param name="time">Time.</param>
        /// <returns>Total OTU.</returns>
        public static double TotalOtu(double otuMin, double time)
        {
            return otuMin * time;
        }

        /// <summary>
        /// Converts metres to feet.
        /// </summary>
        /// <param name="metres">Depth in metres.</param>
        /// <returns>Depth in feet.</returns>
        public static double MetresToFeet(double metres)
        {
            return metres * 3.28084;
        }

        /// <summary>
        /// Converts feet to metres.
        /// </summary>
        /// <param name="feet">Depth in feet.</param>
        /// <returns>Depth in metres.</returns>
        public static double FeetToMetres(double feet)
        {
            return feet / 3.28084;
        }
    }

    public class MetricFormulas
    {
        /// <summary>
        /// Equivalent air depth.
        /// </summary>
        /// <param name="o2">Fraction of oxygen.</param>
        /// <param name="depth">Depth.</param>
        /// <returns>Equivalent air depth in meters.</returns>
        public static double Ead(double o2, double depth)
        {
            return ((1 - o2) * (depth + 10) / 0.79) - 10;
        }

        /// <summary>
        /// Maximum depth for bottom gas.
        /// </summary>
        /// <param name="o2">Fraction of oxygen.</param>
        /// <param name="po2">Max PPO2 at depth</param>
        /// <returns>Maximum depth for bottom gas in meters.</returns>
        public static double MaxDepth(double o2, double po2)
        {
            return ((po2 * 10) / (o2 / 100)) - 10;
        }

        /// <summary>
        /// Surface air consumption rate.
        /// </summary>
        /// <param name="bar">Bar.</param>
        /// <param name="cylinder">Cylinder.</param>
        /// <param name="depth">Depth.</param>
        /// <param name="time">Time.</param>
        /// <returns>Surface air consumption rate in L/Min.</returns>
        public static double SacRate(double bar, double cylinder, double depth, double time)
        {
            return (bar * cylinder) / ((depth + 10) / 10) / time;
        }

        /// <summary>
        /// Gas requirement estimate.
        /// </summary>
        /// <param name="time">Time.</param>
        /// <param name="sac">Surface air consumption rate.</param>
        /// <param name="depth">Depth.</param>
        /// <returns>Gas requirement estimate in L.</returns>
        public static double GasRequirementEstimate(double time, double sac, double depth)
        {
            return (time * sac) * ((depth + 10) / 10);
        }

        /// <summary>
        /// Actual gas supply.
        /// </summary>
        /// <param name="volume">Volume of gas.</param>
        /// <param name="bar">Bar.</param>
        /// <returns>Actual gas supply in L.</returns>
        public static double ActualGasSupply(double volume, double bar)
        {
            return volume * bar;
        }

        /// <summary>
        /// Turn pressure.
        /// </summary>
        /// <param name="start">Starting pressure.</param>
        /// <param name="bottom">Bottom depth.</param>
        /// <param name="cylinder">Cylinder.</param>
        /// <returns>Turn pressure in bar.</returns>
        public static double TurnPressure(double start, double bottom, double cylinder)
        {
            return start - (bottom / cylinder);
        }
        /// <summary>
        /// Convert ATA to depth of metres.
        /// </summary>
        /// <param name="ata"></param>
        /// <returns></returns>
        public static double AtaToDepth(double ata)
        {
            return (ata - 1) * 10.3;
        }
        /// <summary>
        /// Converts depth in meters to ATA.
        /// </summary>
        /// <param name="ata"></param>
        /// <returns></returns>
        public static double DepthToAta(double ata)
        {
            return (ata / 10.3) + 1;
        }
    }

    public class ImperialFormulas
    {
        /// <summary>
        /// Equivalent air depth.
        /// </summary>
        /// <param name="o2">Fraction of oxygen.</param>
        /// <param name="depth">Depth.</param>
        /// <returns>Equivalent air depth in feet.</returns>
        public static double Ead(double o2, double depth)
        {
            return ((1 - o2) * (depth + 33) / 0.79) - 33;
        }

        /// <summary>
        /// Maximum depth for bottom gas.
        /// </summary>
        /// <param name="o2">Fraction of oxygen.</param>
        /// <param name="po2">Max parial pressure of oxygen.</param>
        /// <returns>Maximum depth for gas in feet.</returns>
        public static double MaxDepth(double o2, double po2)
        {
            return (po2 * 33 / (o2 / 100)) - 33;
        }

        /// <summary>
        /// Surface air consumption rate.
        /// </summary>
        /// <param name="psi">PSI.</param>
        /// <param name="wPressure">Working pressure.</param>
        /// <param name="cylinder">Cylinder.</param>
        /// <param name="depth">Depth.</param>
        /// <param name="time">Time.</param>
        /// <returns>Surface air consumption rate in Cu Ft/Min.</returns>
        public static double SacRate(double psi, double wPressure, double cylinder, double depth, double time)
        {
            return ((psi / wPressure) * cylinder) / ((depth + 33) / 33) / time;
        }

        /// <summary>
        /// Gas requirement estimate.
        /// </summary>
        /// <param name="time">Time.</param>
        /// <param name="sac">Surface air consumption rate.</param>
        /// <param name="depth">Depth.</param>
        /// <returns>Gas requirement estimate in Cu Ft.</returns>
        public static double GasRequirementEstimate(double time, double sac, double depth)
        {
            return (time * sac) * ((depth + 33) / 33);
        }

        /// <summary>
        /// Actual gas supply.
        /// </summary>
        /// <param name="volume">Volume of gas.</param>
        /// <param name="psi">PSI.</param>
        /// <param name="wPressure">Working pressure.</param>
        /// <returns>Actual gas supply in Cu Ft.</returns>
        public static double ActualGasSupply(double volume, double psi, double wPressure)
        {
            return psi / wPressure * volume;
        }

        /// <summary>
        /// Convert ATA to depth of feet.
        /// </summary>
        /// <param name="ata"></param>
        /// <returns></returns>
        public static double AtaToDepth(double ata)
        {
            return (ata - 1) * 33.79265;
        }

        /// <summary>
        /// Converts depth in feet to ATA.
        /// </summary>
        /// <param name="ata"></param>
        /// <returns></returns>
        public static double DepthToAta(double ata)
        {
            return (ata / 33.79265) + 1;
        }
    }
}
