namespace ToDo.Wasm
{
    using Uno.UI;

    public static class Program
    {
        private static App _app;

        private static int Main(string[] args)
        {
            FeatureConfiguration.UIElement.AssignDOMXamlName = true;
            Windows.UI.Xaml.Application.Start(_ => _app = new App());

            return 0;
        }
    }
}
