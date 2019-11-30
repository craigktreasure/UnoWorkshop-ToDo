#if __WASM__
namespace ToDo.Controls
{
    using Uno.Extensions;
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls;

    // 📚 <see cref="https://developer.mozilla.org/en-US/docs/Web/HTML/Element/progress"/>
    public partial class NativeProgress : Control
    {
        public NativeProgress()
            : base ("progress")
        {
            this.MinHeight = 20;
            this.HorizontalAlignment = HorizontalAlignment.Stretch;

            this.UpdateAttributes();
        }

        private static void OnValueChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs args)
        {
            (dependencyObject as NativeProgress)?.UpdateAttributes();
        }

        private static void OnMinChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs args)
        {
            (dependencyObject as NativeProgress)?.UpdateAttributes();
        }

        private static void OnMaxChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs args)
        {
            (dependencyObject as NativeProgress)?.UpdateAttributes();
        }

        private void UpdateAttributes()
        {
            // The minimum value is always 0 and the min attribute is not allowed for progress elements
            // https://developer.mozilla.org/en-US/docs/Web/HTML/Element/progress#Attributes

            // To override this limitation, we recalculate the value & max based on our minimum.
            double min = this.Minimum;

            double calculatedValue = this.Value - min;
            double calculatedMax = this.Maximum - min;

            // 🛈 Usage: SetAttribute("HtmlAttributeName", "Value");
            this.SetAttribute("Value", calculatedValue.ToStringInvariant());
			this.SetAttribute("Max", calculatedMax.ToStringInvariant());
        }
    }
}
#endif