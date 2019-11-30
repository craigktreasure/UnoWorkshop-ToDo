namespace ToDo.Converters
{
    using System;
    using System.Collections;
    using Uno.Extensions.Specialized;
    using Windows.UI.Xaml.Data;

    public class FromEnumerableToCountConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is IEnumerable enumerable)
            {
                int count = enumerable.Count();
                return count == 0 ? "" : $" ({count})";
            }
            else
            {
                return null;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotSupportedException();
        }
    }
}