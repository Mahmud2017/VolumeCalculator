using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VolumeCalculator.Interfaces
{
    /// <summary>
    /// Interface for volume calculation helper.
    /// </summary>
    public interface IVolumeCalculationHelper
    {
        void InitialCalculation(List<int> topHorizonList);
        double GetCubicFeet();
        double GetCubicMeter();
        double GetBarrels();
    }
}
