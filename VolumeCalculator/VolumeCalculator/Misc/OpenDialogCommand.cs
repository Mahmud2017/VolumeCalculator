using Microsoft.Win32;
using System;
using System.Windows.Input;

namespace VolumeCalculator.Misc
{
    /// <summary>
    /// Handles the dialog for opening a file.
    /// </summary>
    public class OpenDialogCommand : ICommand
    {
        #region Private Members
        private readonly Func<bool> m_canExecute;
        private readonly Action<string> m_opened;
        private OpenFileDialog filedialog;
        #endregion

        #region Public Events
        public event EventHandler CanExecuteChanged;
        #endregion

        #region Constructor
        public OpenDialogCommand(Func<bool> canExecute, Action<string> opened)
        {
            m_canExecute = canExecute;
            m_opened = opened;
        }
        #endregion

        #region Public Methods
        public bool CanExecute(object parameter)
        {
            return m_canExecute == null ? true : m_canExecute();
        }

        public void Execute(object parameter)
        {
            if (CanExecute(parameter))
            {
                if (filedialog == null)
                {
                    filedialog = new OpenFileDialog();
                    filedialog.InitialDirectory = @"C:\";
                    filedialog.Title = "Browse Files";

                    filedialog.CheckFileExists = true;
                    filedialog.CheckPathExists = true;

                    filedialog.Filter = "csv files (*.csv)|*.csv|text files (*.txt)|*.txt";
                    filedialog.FilterIndex = 1;
                    filedialog.RestoreDirectory = true;
                }

                if (filedialog.ShowDialog() == true)
                {
                    m_opened?.Invoke(filedialog.FileName);
                }
            }
        }

        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
        #endregion
    }
}
