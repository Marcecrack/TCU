namespace Library.Converters
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using Xamarin.Forms;
    using Models.GoogleModels;

    public class NullToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (bool) value ? true: false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
