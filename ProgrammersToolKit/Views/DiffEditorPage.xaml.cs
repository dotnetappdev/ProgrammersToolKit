using Avalonia.Interactivity;
using Avalonia.Controls;
using Avalonia.Controls;

namespace ProgrammersToolKit.Views
{
    public partial class DiffEditorPage : UserControl
    {
        public DiffEditorPage()
        {
            this.InitializeComponent();
        }

        private void CompareButton_Click(object sender, RoutedEventArgs e)
        {
            var left = LeftTextBox.Text;
            var right = RightTextBox.Text;
            // TODO: Add diff logic and highlight differences
            // For now, just show a message dialog
            var dialog = new ContentDialog
            {
                Title = "Diff Result",
                Content = left == right ? "No differences found." : "Differences detected.",
                CloseButtonText = "OK"
            };
            _ = dialog.ShowAsync();
        }
    }
}
