using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using ProgrammersToolKit.Services.Interfaces;

namespace ProgrammersToolKit.Views
{
    public sealed partial class HexEditorPage : Page
    {
        private readonly IHexEditorService _service;
        public HexEditorPage()
        {
            this.InitializeComponent();
            _service = (IHexEditorService)AppHost.ServiceProvider.GetService(typeof(IHexEditorService));
        }

        private void ToText_Click(object sender, RoutedEventArgs e)
        {
            var input = HexInputBox.Text ?? string.Empty;
            try
            {
                TextOutputBox.Text = _service.HexToText(input);
            }
            catch (Exception ex)
            {
                TextOutputBox.Text = $"Error: {ex.Message}";
            }
        }

        private void ToHex_Click(object sender, RoutedEventArgs e)
        {
            var input = TextOutputBox.Text ?? string.Empty;
            try
            {
                HexInputBox.Text = _service.TextToHex(input);
            }
            catch (Exception ex)
            {
                HexInputBox.Text = $"Error: {ex.Message}";
            }
        }
    }
}
