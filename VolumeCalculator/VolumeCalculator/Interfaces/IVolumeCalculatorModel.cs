using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VolumeCalculator.Interfaces
{
    /// <summary>
    /// Interface for volume calculator model.
    /// </summary>
    public interface IVolumeCalculatorModel
    {
        string GetResult();
        List<int> GetImportedData();
        void SetResult(string result);
        void SetImportedData(List<int> dataList);
    }
}
