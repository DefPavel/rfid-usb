using Microsoft.Xaml.Behaviors;
using System;
using System.Windows.Controls;
using System.Windows.Threading;

namespace WpfApp2
{
    public class LineLimitingBehavior : Behavior<TextBox>
    {

        public int? TextBoxMaxAllowedLines { get; set; }
        protected override void OnAttached()
        {
            if (TextBoxMaxAllowedLines != null && TextBoxMaxAllowedLines > 0)
            {
                AssociatedObject.TextChanged += OnTextBoxTextChanged;
            }
        }

        protected override void OnDetaching()
        {
            AssociatedObject.TextChanged -= OnTextBoxTextChanged;
        }

        private void OnTextBoxTextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;

            int textLineCount = textBox.Text.Length;

            //Use Dispatcher to undo - http://stackoverflow.com/a/25453051/685341
            if (textLineCount > TextBoxMaxAllowedLines.Value)
            {
                // Очистить после
                _ = Dispatcher.BeginInvoke(DispatcherPriority.Input, (Action)(() => textBox.Clear()));
            }
        }

    }
}
