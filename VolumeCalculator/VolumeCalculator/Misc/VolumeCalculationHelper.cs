using System.Collections.Generic;
using VolumeCalculator.Interfaces;

namespace VolumeCalculator.Misc
{
    /// <summary>
    /// Handles all the calculations for volume.
    /// Contains all the volume data.
    /// </summary>
    class VolumeCalculationHelper : IVolumeCalculationHelper
    {
        #region Private Constant Members        
        private const double METER_TO_FEET = 3.28084;
        private const int LATERAL_GRID_CELL_AREA = Globals.LATERAL_GRID_CELL_X * Globals.LATERAL_GRID_CELL_Y;
        private const double HORIZON_DIFF_FEET = Globals.HORIZON_DIFF_METER * METER_TO_FEET;
        private const double FLUID_CONTACT_FEET = Globals.FLUID_CONTACT_METER * METER_TO_FEET;
        private const double CUBIC_FEET_TO_CUBIC_METER = 0.0283168466;
        private const double CUBIC_FEET_TO_BARRELS = 0.237476809;
        #endregion

        #region Private Members
        private double m_CubicFeetVolume = 0;
        #endregion

        #region Public Properties
        public double GetCubicFeet()
        {
            return m_CubicFeetVolume;
        }

        public double GetCubicMeter()
        {
            return m_CubicFeetVolume * CUBIC_FEET_TO_CUBIC_METER;
        }

        public double GetBarrels()
        {
            return m_CubicFeetVolume * CUBIC_FEET_TO_BARRELS;
        }
        #endregion

        #region Public Methods
        public void InitialCalculation(List<int> topHorizonList)
        {
            double bottomHorizon = 0;
            double sum = 0;

            topHorizonList.ForEach(aTopHorizon =>
            {
                bottomHorizon = aTopHorizon + HORIZON_DIFF_FEET;
                sum += aTopHorizon >= FLUID_CONTACT_FEET ? 0.0 : bottomHorizon > FLUID_CONTACT_FEET ? FLUID_CONTACT_FEET - aTopHorizon : bottomHorizon - aTopHorizon;
            });

            m_CubicFeetVolume = sum * LATERAL_GRID_CELL_AREA;
        }
        #endregion
    }
}