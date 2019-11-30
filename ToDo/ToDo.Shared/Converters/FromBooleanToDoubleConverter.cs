namespace ToDo.Converters
{
    using System;
    using System.Globalization;
    using Windows.UI.Xaml.Data;

    public class FromBooleanToDoubleConverter : IValueConverter
    {
        public double TrueValue { get; } = 0.5;
        public double FalseValue { get; } = 1;

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is bool boolValue)
            {
                return boolValue ? this.TrueValue : this.FalseValue;
            }

            return System.Convert.ToBoolean(value, CultureInfo.InvariantCulture) ? this.TrueValue : this.FalseValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotSupportedException();
        }
    }
}