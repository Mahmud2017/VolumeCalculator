using System;
using System.Collections.Generic;
using VolumeCalculator.Interfaces;

namespace VolumeCalculator.Model
{
    /// <summary>
    /// Contains the data for volume calculator.
    /// This is the model.
    /// </summary>
    public class VolumeCalculatorModel : IVolumeCalculatorModel
    {
        private string result;
        private List<int> importedData;

        public VolumeCalculatorModel()
        {
            result = "";
            importedData = new List<int>();
        }
        public string GetResult()
        {
            return result;
        }
        public List<int> GetImportedData()
        {
            return importedData;
        }

        public void SetResult(string result)
        {
            this.result = result;
        }

        public void SetImportedData(List<int> dataList)
        {
            importedData = dataList;
        }
    }
}