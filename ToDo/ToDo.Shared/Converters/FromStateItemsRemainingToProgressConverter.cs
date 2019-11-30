namespace ToDo.Converters
{
    using System;
    using ToDo.Models;
    using Windows.UI.Xaml.Data;

    public class FromStateItemsRemainingToProgressConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is State state)
            {
                int total = state.Todos.Length;

                if (total == 0)
                {
                    return 0; // prevent a division by zero
                }

                int remaining = state.RemainingTodos;

                if (remaining == 0)
                {
                    return 100; // prevent useless calculation
                }

                double completedRatio = (double)(total - remaining) / total;
                int percentComplete = (int)Math.Round(100.0d * completedRatio);

                return percentComplete;
            }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotSupportedException();
        }
    }
}