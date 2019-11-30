namespace ToDo.Controls
{
    using Windows.UI.Xaml;

#if !NETFX_CORE
    public partial class NativeProgress
    {
        public static readonly DependencyProperty ValueProperty = DependencyProperty.Register(
            nameof(Value), typeof(double), typeof(NativeProgress), new PropertyMetadata(50.0d, OnValueChanged));

        public static readonly DependencyProperty MinProperty = DependencyProperty.Register(
            nameof(Minimum), typeof(double), typeof(NativeProgress), new PropertyMetadata(0d, OnMinChanged));

        public static readonly DependencyProperty MaxProperty = DependencyProperty.Register(
            nameof(Maximum), typeof(double), typeof(NativeProgress), new PropertyMetadata(100.0d, OnMaxChanged));

        public double Value
        {
            get => (double)this.GetValue(ValueProperty);
            set => this.SetValue(ValueProperty, value);
        }

        public double Minimum
        {
            get => (double)this.GetValue(MinProperty);
            set => this.SetValue(MinProperty, value);
        }

        public double Maximum
        {
            get => (double)this.GetValue(MaxProperty);
            set => this.SetValue(MaxProperty, value);
        }
    }
#endif
}