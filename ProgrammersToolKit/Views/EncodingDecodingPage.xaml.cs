using Avalonia.Interactivity;
using Avalonia.Controls;
using Avalonia.Controls;
using System;
using System.Text;
using System.Web;

namespace ProgrammersToolKit.Views
{
    public partial class EncodingDecodingPage : UserControl
    {
        private readonly ProgrammersToolKit.Services.Interfaces.IEncodingDecodingService _service;

        public EncodingDecodingPage()
        {
            this.InitializeComponent();
            _service = (ProgrammersToolKit.Services.Interfaces.IEncodingDecodingService)AppHost.ServiceProvider.GetService(typeof(ProgrammersToolKit.Services.Interfaces.IEncodingDecodingService));
        }

        private void Encode_Click(object sender, RoutedEventArgs e)
        {
            var input = InputBox.Text ?? string.Empty;
            var encoding = (EncodingBox.SelectedItem as ComboBoxItem)?.Content?.ToString();
            if (string.IsNullOrWhiteSpace(encoding)) return;
            try
            {
                OutputBox.Text = _service.Encode(input, encoding);
            }
            catch (Exception ex)
            {
                OutputBox.Text = $"Error: {ex.Message}";
            }
        }

        private void Decode_Click(object sender, RoutedEventArgs e)
        {
            var input = InputBox.Text ?? string.Empty;
            var encoding = (EncodingBox.SelectedItem as ComboBoxItem)?.Content?.ToString();
            if (string.IsNullOrWhiteSpace(encoding)) return;
            try
            {
                OutputBox.Text = _service.Decode(input, encoding);
            }
            catch (Exception ex)
            {
                OutputBox.Text = $"Error: {ex.Message}";
            }
        }
    }
}
