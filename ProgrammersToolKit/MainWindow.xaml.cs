using Avalonia.Controls;
using Avalonia.Interactivity;
using System;

namespace ProgrammersToolKit
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            // Set initial page
            LoadPage("ApiTestPage");
        }

        private void MainNavList_SelectionChanged(object? sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0 && e.AddedItems[0] is ListBoxItem item && item.Tag is string tag)
            {
                LoadPage(tag);
            }
        }

        private void LoadPage(string pageTag)
        {
            UserControl? page = pageTag switch
            {
                "ApiTestPage" => new Views.ApiTestPage(),
                "TestResultsPage" => new Views.TestResultsPage(),
                "ToolsPage" => new Views.ToolsPage(),
                "HeaderInspectorPage" => new Views.HeaderInspectorPage(),
                "EncryptionToolPage" => new Views.EncryptionToolPage(),
                "HexEditorPage" => new Views.HexEditorPage(),
                "EncodingDecodingPage" => new Views.EncodingDecodingPage(),
                "CookieInspectorPage" => new Views.CookieInspectorPage(),
                "CodeRunnerPage" => new Views.CodeRunnerPage(),
                "DiffEditorPage" => new Views.DiffEditorPage(),
                "JsonVisualizerPage" => new Views.JsonVisualizerPage(),
                "SqlQueryWindow" => new Views.SqlQueryWindow(),
                _ => new Views.ApiTestPage()
            };

            ContentFrame.Content = page;
        }
    }
}
