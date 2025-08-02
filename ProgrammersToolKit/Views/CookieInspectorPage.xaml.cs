using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using ProgrammersToolKit.Services.Interfaces;

namespace ProgrammersToolKit.Views
{
    public sealed partial class CookieInspectorPage : Page
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
