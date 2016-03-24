using System;
using System.Globalization;
using System.Windows.Data;
namespace ValueConverters
{
    public class EnumConverter: IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            //foreach (var one in Enum.GetValues(parameter as Type))
            //{
            //    if (value.Equals(onve))
            //    {
            //        return one.
            //    }
            //}
            throw new NotImplementedException();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
