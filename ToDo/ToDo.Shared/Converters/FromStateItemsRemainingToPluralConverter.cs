namespace ToDo.Converters
{
    using System;
    using ToDo.Models;
    using Windows.UI.Xaml.Data;

    public class FromStateItemsRemainingToPluralConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is State state)
            {
                int amountRemaining = state.RemainingTodos;
                return amountRemaining == 1 ? $"{amountRemaining} item left" : $"{amountRemaining} items left";
            }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotSupportedException();
        }
    }
}