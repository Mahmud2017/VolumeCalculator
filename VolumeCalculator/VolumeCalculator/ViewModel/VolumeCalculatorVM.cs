using System;
using System.Collections.Generic;
using System.IO;
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

        public bool InTestMode { get; set; } = false;
        #endregion

        #region Constructor

        /// <summary>
        /// Constructor of the view model class. Initializes all the required members with the passed references.
        /// </summary>
        /// <param name="volumeCalculationHelper">Helps to calculate the volume.</param>
        /// <param name="volumeCalculatorModel">This is the data class. This holds the required data.</param>
        /// <param name="readFile">Helps to read a file.</param>
        public VolumeCalculatorVM(IVolumeCalculationHelper volumeCalculationHelper, IVolumeCalculatorModel volumeCalculatorModel, IReadFile readFile)
        {
            m_VolumeCalculationHelper = volumeCalculationHelper;
            m_VolumeCalculatorModel = volumeCalculatorModel;
            m_ReadCSV = readFile;
            OpenDialogCmd = new OpenDialogCommand(null, SetImportText);
        }
        #endregion

        #region Private Methods

        /// <summary>
        /// Triggered after a path is selected with open command on the dialog menu.
        /// </summary>
        /// <param name="importedPath"></param>
        private void SetImportText(string importedPath)
        {
            ImportText = importedPath;
        }

        /// <summary>
        /// Returns the given file path is valid or, not.
        /// </summary>
        /// <param name="path">Path of the file</param>
        /// <returns></returns>
        private bool PathIsValid(string path)
        {
            return m_ReadCSV.PathIsValid(path);
        }

        /// <summary>
        /// Imports the data from the file.
        /// <exception cref="System.IO.InvalidDataException">Thrown when the amount of rowXcolumn is not 16X26.</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">Thrown when data is negative.</exception>
        /// <exception cref="System.FormatException">Thrown when data is null or, empty or, not int.</exception>
        /// <exception cref="System.OverflowException">Thrown when data is too large or, too small for an Int32.</exception>
        /// </summary>
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

                ImportedData = m_TopHorizonList;
                m_VolumeCalculationHelper.InitialCalculation(m_TopHorizonList);
                IsUnitsBoxEnabled = true;
                IsCubicFeetSelected = true;
            }
            catch(InvalidDataException e)
            {
                ShowErrorMessage("File does not contains the correct amount of data.", e);
            }
            catch (ArgumentOutOfRangeException e)
            {
                ShowErrorMessage("File contains negative data.", e);
            }
            catch (FormatException e)
            {
                ShowErrorMessage("File contains corrupt data.", e);
            }
            catch (OverflowException e)
            {
                ShowErrorMessage("File contains too large or, too small data.", e);
            }
        }

        /// <summary>
        /// Show error message when there is any exception.
        /// </summary>
        private void ShowErrorMessage(string errorMessage, Exception e)
        {
            ImportText = "";

            if (!InTestMode)
                MessageBox.Show(errorMessage, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            else
                throw e;
        }

        /// <summary>
        /// Sets cubic feet as the result.
        /// </summary>
        private void SetCubicFeetAsResult()
        {            
            ResultText = m_VolumeCalculationHelper.GetCubicFeet().ToString("0.00");
        }

        /// <summary>
        /// Sets cubic meter as the result.
        /// </summary>
        private void SetCubicMeterAsResult()
        {
            ResultText = m_VolumeCalculationHelper.GetCubicMeter().ToString("0.00");
        }

        /// <summary>
        /// Sets barrels as the result.
        /// </summary>
        private void SetBarrelsAsResult()
        {
            ResultText = m_VolumeCalculationHelper.GetBarrels().ToString("0.00");
        }
        #endregion
    }
}