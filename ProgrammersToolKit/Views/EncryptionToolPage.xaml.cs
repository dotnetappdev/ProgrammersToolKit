using Avalonia.Interactivity;
using Avalonia.Controls;
using Avalonia.Controls;
using ProgrammersToolKit.Services.Interfaces;

namespace ProgrammersToolKit.Views
{
    public partial class EncryptionToolPage : UserControl
    {
        private readonly IEncryptionToolService _service;
        public EncryptionToolPage()
        {
            this.InitializeComponent();
            _service = (IEncryptionToolService)AppHost.ServiceProvider.GetService(typeof(IEncryptionToolService));
        }

        private void Encrypt_Click(object sender, RoutedEventArgs e)
        {
            var input = InputBox.Text ?? string.Empty;
            var key = KeyBox.Text ?? string.Empty;
            var algorithm = (AlgorithmBox.SelectedItem as ComboBoxItem)?.Content?.ToString();
            if (string.IsNullOrWhiteSpace(algorithm)) return;
            try
            {
                OutputBox.Text = _service.Encrypt(input, key, algorithm);
            }
            catch (Exception ex)
            {
                OutputBox.Text = $"Error: {ex.Message}";
            }
        }

        private void Decrypt_Click(object sender, RoutedEventArgs e)
        {
            var input = InputBox.Text ?? string.Empty;
            var key = KeyBox.Text ?? string.Empty;
            var algorithm = (AlgorithmBox.SelectedItem as ComboBoxItem)?.Content?.ToString();
            if (string.IsNullOrWhiteSpace(algorithm)) return;
            try
            {
                OutputBox.Text = _service.Decrypt(input, key, algorithm);
            }
            catch (Exception ex)
            {
                OutputBox.Text = $"Error: {ex.Message}";
            }
        }
    }
}
