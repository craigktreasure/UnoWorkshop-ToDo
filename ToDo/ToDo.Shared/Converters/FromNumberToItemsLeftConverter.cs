namespace ToDo.Converters
{
    using Humanizer;
    using System;
    using Windows.UI.Xaml.Data;

    public class FromNumberToItemsLeftConverter : IValueConverter
    {
        private const string DefaultSubjectType = "item";

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            string subjectName = parameter is string parameterSubjectName ? parameterSubjectName : DefaultSubjectType;

            if (value is int amountRemaining)
            {
                if (amountRemaining != 1)
                {
                    subjectName = subjectName.Pluralize(inputIsKnownToBeSingular: false);
                }

                return $"{amountRemaining} {subjectName} left";
            }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotSupportedException();
        }
    }
}