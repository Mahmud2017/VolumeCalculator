using System.Windows;
using VolumeCalculator.Interfaces;
using VolumeCalculator.Misc;
using VolumeCalculator.Model;
using VolumeCalculator.ViewModel;

namespace VolumeCalculator
{
    /// <summary>
    /// This is the application.
    /// </summary>
    public partial class App : Application
    {
        /// <summary>
        /// Called when the applicaiton starts.
        /// </summary>
        /// <param name="e">Argumanets for the startup</param>
        protected override void OnStartup(StartupEventArgs e)
        {
            //Created all the interface objects
            IReadFile irf = new ReadFile();
            IVolumeCalculationHelper ivch = new VolumeCalculationHelper();
            IVolumeCalculatorModel ivcm = new VolumeCalculatorModel();

            //Created the view model object and passed the interfaces in the constructor
            VolumeCalculatorVM vm = new VolumeCalculatorVM(ivch, ivcm, irf);

            //Created the main window object
            MainWindow mw = new VolumeCalculator.MainWindow();

            //Added the view model object as Data Context of the main window
            mw.DataContext = vm;

            //Show the main window
            mw.Show();
        }
    }
}
