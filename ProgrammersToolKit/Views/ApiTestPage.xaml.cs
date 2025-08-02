using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using ProgrammersToolKit.Core;
using ProgrammersToolKit.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProgrammersToolKit.Views
{
    public sealed partial class ApiTestPage : Page
    {
        private readonly IApiTestRepository _repo;
        private readonly IApiTestRunner _runner;
        public ApiTestPage()
        {
            this.InitializeComponent();
            _repo = (IApiTestRepository)AppHost.ServiceProvider.GetService(typeof(IApiTestRepository));
            _runner = (IApiTestRunner)AppHost.ServiceProvider.GetService(typeof(IApiTestRunner));
            LoadTests();
        }

        private async void LoadTests()
        {
            ApiTestListBox.ItemsSource = await _repo.GetAllTestsAsync();
        }

        private async void SaveTest_Click(object sender, RoutedEventArgs e)
        {
            var test = new ApiTestDefinition
            {
                Name = TestNameBox.Text,
                Url = TestUrlBox.Text,
                Method = (TestMethodBox.SelectedItem as ComboBoxItem)?.Content?.ToString(),
                HeadersJson = TestHeadersBox.Text,
                Body = TestBodyBox.Text,
                BodyType = (TestBodyTypeBox.SelectedItem as ComboBoxItem)?.Content?.ToString(),
                Assertions = TestAssertionsBox.Text,
                TestDate = DateTime.Now
            };
            await _repo.AddTestAsync(test);
            LoadTests();
        }

        private async void DeleteTest_Click(object sender, RoutedEventArgs e)
        {
            if (ApiTestListBox.SelectedItem is ApiTestDefinition test)
            {
                await _repo.DeleteTestAsync(test.Id);
                LoadTests();
            }
        }

        private async void RunSelectedTest_Click(object sender, RoutedEventArgs e)
        {
            if (ApiTestListBox.SelectedItem is ApiTestDefinition test)
            {
                var result = await _runner.RunTestAsync(test);
                // Show result in a dialog or navigate to detail page
                var dialog = new ContentDialog
                {
                    Title = $"Result: {(result.Success ? "PASS" : "FAIL")}",
                    Content = $"Status: {result.StatusCode}\nBody: {result.ResponseBody}",
                    CloseButtonText = "OK"
                };
                await dialog.ShowAsync();
            }
        }
    }
}
