using Avalonia.Interactivity;
using Avalonia.Controls;
using Avalonia.Controls;
using ProgrammersToolKit.Services.Interfaces;

namespace ProgrammersToolKit.Views
{
    public partial class CookieInspectorPage : UserControl
    {
        private readonly ICookieInspectorService _service;
        public CookieInspectorPage()
        {
            this.InitializeComponent();
            _service = (ICookieInspectorService)AppHost.ServiceProvider.GetService(typeof(ICookieInspectorService));
        }

        private void ParseCookie_Click(object sender, RoutedEventArgs e)
        {
            var input = CookieInputBox.Text ?? string.Empty;
            ParsedCookieListView.ItemsSource = _service.ParseCookie(input);
        }
    }
}
