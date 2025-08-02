using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;

namespace ProgrammersToolKit.Views
{
    public sealed partial class HeaderInspectorPage : Page
    {
        private readonly ProgrammersToolKit.Services.Interfaces.IHeaderInspectorService _service;

        public HeaderInspectorPage()
        {
            this.InitializeComponent();
            _service = (ProgrammersToolKit.Services.Interfaces.IHeaderInspectorService)AppHost.ServiceProvider.GetService(typeof(ProgrammersToolKit.Services.Interfaces.IHeaderInspectorService));
        }

        private void ParseHeaders_Click(object sender, RoutedEventArgs e)
        {
            var input = HeaderInputBox.Text ?? string.Empty;
            ParsedHeadersListView.ItemsSource = _service.ParseHeaders(input);
        }
    }
}
