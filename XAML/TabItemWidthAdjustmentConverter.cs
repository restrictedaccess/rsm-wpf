using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace RSM
{
    class TabItemWidthAdjustmentConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            Double lTabControlWidth = value is Double ? (Double)value : 50; // 50 just to see something, in case of error
            Int32 lTabsCount = (parameter != null && parameter is String) ? Int32.Parse((String)parameter) : 1;
            return lTabControlWidth / lTabsCount;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
