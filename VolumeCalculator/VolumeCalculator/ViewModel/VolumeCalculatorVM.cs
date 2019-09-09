using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using VolumeCalculator.Interfaces;
using VolumeCalculator.Misc;
using VolumeCalculator.Model;

namespace VolumeCalculator.ViewModel
{
    /// <summary>
    /// Works as the bridge between view and model.
    /// The view knows about this class and this class knows about the model.
    /// This is the view model.
    /// Contains all the members and functionality to modify view and model.
    /// </summary>
    public class VolumeCalculatorVM : BaseINPC
    {
        #region Private Members
        private IVolumeCalculatorModel m_VolumeCalculatorModel;
        private IVolumeCalculationHelper m_VolumeCalculationHelper;
        private IReadFile m_ReadCSV;
        private List<int> m_TopHorizonList;
        private bool m_IsCubicFeetSelected = false;
        private bool m_IsCubicMeterSelected = false;
        private bool m_IsBarrelsSelected = false;
        private bool m_IsUnitsBoxEnabled = false;
        private string m_ImportedPath = "";
        #endregion

        #region Public Properties
        public OpenDialogCommand OpenDialogCmd { get; set; }

        public string ResultText
        {
            get { return m_VolumeCalculatorModel.GetResult(); }
            set
            {
                m_VolumeCalculatorModel.SetResult(value);
                RaisePropertyChanged("ResultText");
            }
        }
        public string ImportText
        {
            get { return m_ImportedPath; }
            set
            {
                if (PathIsValid(value))
                {
                    m_ImportedPath = value;
                    RaisePropertyChanged("ImportText");

                    if (!string.IsNullOrEmpty(m_ImportedPath))
                        ImportData();                    
                }
            }                
        }
        public bool IsCubicFeetSelected
        {
            get { return m_IsCubicFeetSelected; }
            set
            {
                m_IsCubicFeetSelected = value;

                if (m_IsCubicFeetSelected)
                    SetCubicFeetAsResult();

                RaisePropertyChanged("IsCubicFeetSelected");
            }
        }
        public bool IsCubicMeterSelected
        {
            get { return m_IsCubicMeterSelected; }
            set
            {
                m_IsCubicMeterSelected = value;

                if (m_IsCubicMeterSelected)
                    SetCubicMeterAsResult();

                RaisePropertyChanged("IsCubicMeterSelected");
            }
        }
        public bool IsBarrelsSelected
        {
            get { return m_IsBarrelsSelected; }
            set
            {
                m_IsBarrelsSelected = value;

                if (m_IsBarrelsSelected)
                    SetBarrelsAsResult();

                RaisePropertyChanged("IsBarrelsSelected");
            }
        }
        public bool IsUnitsBoxEnabled
        {
            get { return m_IsUnitsBoxEnabled; }
            set
            {
                m_IsUnitsBoxEnabled = value;
                RaisePropertyChanged("IsUnitsBoxEnabled");
            }
        }

        public List<int> ImportedData
        {
            get { return m_VolumeCalculatorModel.GetImportedData(); }
            set
            {
                m_VolumeCalculatorModel.SetImportedData(value);
                RaisePropertyChanged("ImportedData");
            }
        }
        #endregion

        #region Constructor
        public VolumeCalculatorVM(IVolumeCalculationHelper volumeCalculationHelper, IVolumeCalculatorModel volumeCalculatorModel, IReadFile readFile)
        {
            m_VolumeCalculationHelper = volumeCalculationHelper;
            m_VolumeCalculatorModel = volumeCalculatorModel;
            m_ReadCSV = readFile;
            OpenDialogCmd = new OpenDialogCommand(null, SetImportText);
        }
        #endregion

        #region Private Methods
        private void SetImportText(string importedPath)
        {
            ImportText = importedPath;
        }

        private bool PathIsValid(string path)
        {
            return m_ReadCSV.PathIsValid(path);
        }

        private void ImportData()
        {
            try
            {
                IsUnitsBoxEnabled = false;
                IsCubicFeetSelected = false;
                IsCubicMeterSelected = false;
                IsBarrelsSelected = false;
                ResultText = "";
                ImportedData = new List<int>();

                m_TopHorizonList = m_ReadCSV.Read(ImportText);

                if(m_TopHorizonList.Count == 0)
                {
                    ShowErrorMessage("File does not contains the correct amount of data.");
                }
                else if (m_TopHorizonList.Min() < 0)
                {
                    ShowErrorMessage("File contains negative data.");
                }
                else if (m_TopHorizonList.Count > 0)
                {
                    ImportedData = m_TopHorizonList;
                    m_VolumeCalculationHelper.InitialCalculation(m_TopHorizonList);
                    IsUnitsBoxEnabled = true;
                    IsCubicFeetSelected = true;
                }
            }
            catch (Exception ex)
            {                
                ShowErrorMessage("File contains corrupt data.");
            }
        }

        private void ShowErrorMessage(string errorMessage)
        {
            ImportText = "";
            MessageBox.Show(errorMessage, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void SetCubicFeetAsResult()
        {            
            ResultText = m_VolumeCalculationHelper.GetCubicFeet().ToString("0.00");
        }

        private void SetCubicMeterAsResult()
        {
            ResultText = m_VolumeCalculationHelper.GetCubicMeter().ToString("0.00");
        }

        private void SetBarrelsAsResult()
        {
            ResultText = m_VolumeCalculationHelper.GetBarrels().ToString("0.00");
        }
        #endregion
    }
}