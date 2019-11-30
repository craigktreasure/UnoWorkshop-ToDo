namespace ToDo
{
    using Windows.System;
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls;
    using Windows.UI.Xaml.Input;
    using ToDo.Models;
    using ToDo.ViewModels;

    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        private void ItemClicked(object sender, RoutedEventArgs e)
        {
            CheckBox checkBox = sender as CheckBox;
            Todo todo = checkBox?.DataContext as Todo;
            bool isDone = checkBox?.IsChecked ?? false;
            (this.DataContext as MainPageVM)?.ChangeState(todo, isDone);
        }

        private void ChangeItem(object sender, RoutedEventArgs e)
        {
            if (!(sender is TextBlock textBlock))
            {
                return;
            }

            if (!(textBlock.Tag is TextBox textBox))
            {
                return;
            }

            textBox.Visibility = Visibility.Visible;
            textBlock.Visibility = Visibility.Collapsed;

            textBox.LostFocus += UpdateItem;
            textBox.Focus(FocusState.Programmatic); // give focus

            void UpdateItem(object _, RoutedEventArgs __)
            {
                textBox.Visibility = Visibility.Collapsed;
                textBlock.Visibility = Visibility.Visible;

                string newText = textBox.Text;
                Todo todo = textBlock?.DataContext as Todo;
                (this.DataContext as MainPageVM)?.ChangeText(todo, newText);

                textBox.LostFocus -= UpdateItem;
            }
        }

        private void ChangeItemBtn(object sender, RoutedEventArgs e)
        {
            if (sender is Button button)
            {
                this.ChangeItem(button.Tag, null);
            }
        }

        private void DeleteItem(object sender, RoutedEventArgs e)
        {
            if (this.DataContext is MainPageVM vm)
            {
                FrameworkElement button = sender as FrameworkElement;
                Todo todo = button?.DataContext as Todo;

                vm.RemoveTodo(todo);
            }
        }

        private void OnNewTodoKey(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == VirtualKey.Enter)
            {
                if (sender is TextBox textBox)
                {
                    textBox.GetBindingExpression(TextBox.TextProperty)?.UpdateSource();
                    (this.DataContext as MainPageVM)?.CreateNew.Execute(null);
                }
            }
        }

        private void OnItemKey(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key != VirtualKey.Enter)
            {
                return; // not an enter key
            }

            this.Focus(FocusState.Programmatic);

            if (sender is TextBox textBox)
            {
                textBox.GetBindingExpression(TextBox.TextProperty)?.UpdateSource();
            }
        }
    }
}