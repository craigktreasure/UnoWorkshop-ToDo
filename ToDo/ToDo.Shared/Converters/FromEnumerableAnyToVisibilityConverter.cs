namespace ToDo.Converters
{
    using System;
    using System.Collections;
    using Uno.Extensions.Specialized;
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Data;

    public class FromEnumerableAnyToVisibilityConverter : IValueConverter
    {
        public bool IsAnyVisible { get; set; } = true;

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            (object visibleResult, object invisibleResult) results = (true, false);

            if (targetType == typeof(Visibility))
            {
                results = (Visibility.Visible, Visibility.Collapsed);
            }

            if (value is IEnumerable enumerable)
            {
                var isAny = enumerable.Any();

                if (isAny)
                    return IsAnyVisible ? results.visibleResult : results.invisibleResult;
                else
                    return IsAnyVisible ? results.invisibleResult : results.visibleResult;
            }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotSupportedException();
        }
    }
}