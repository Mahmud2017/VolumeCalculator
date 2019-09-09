using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using VolumeCalculator.Misc;

namespace VolumeCalculator.Converters
{
    /// <summary>
    /// Used for converting the list of integer type top horizon data into
    /// a grid structure with rows and columns to show in a textblock.
    /// </summary>
    public class GridStructureConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string txt = "";
            List<int> data = (List<int>)value;
            int index = 0;
            data.ToList().ForEach(n =>
            {
                txt += n + "\t";
                index++;

                if(index % Globals.LATERAL_X == 0)
                    txt += "\n";
            });            

            return txt;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}
