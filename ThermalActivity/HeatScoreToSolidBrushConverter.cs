using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;


namespace ThermalActivity
{
    public class HeatScoreToSolidBrushConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return new SolidColorBrush(Color.FromArgb(byte.Parse(value.ToString()), 255, 0, 0));
        }


        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}
