using System.ComponentModel;

namespace VolumeCalculator.Misc
{
    /// <summary>
    /// A wrapper to use INotifyPropertyChanged.
    /// This is an abstract class.
    /// </summary>
    public abstract class BaseINPC : INotifyPropertyChanged
    {
        protected void RaisePropertyChanged(string propertyName)
        {
            var handler = PropertyChanged;

            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
