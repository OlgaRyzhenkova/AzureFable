using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace AzureFable.Converters
{
    public class CellSizeConverter : IValueConverter
    {
        private const int CellSize = 40;

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value is int coordinate ? coordinate * CellSize : 0;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
