using Avalonia.Controls;
using Avalonia.Interactivity;
using ProgrammersToolKit.Core;
using ProgrammersToolKit.Data;
using ProgrammersToolKit.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProgrammersToolKit.Views
{
    public partial class ApiTestPage : UserControl
    {
        private readonly IApiTestRepository _repo;
        private readonly IApiTestRunner _runner;
        public ApiTestPage()
        {
            InitializeComponent();
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
                // Show result in a simple message for now (could be enhanced with a dialog)
                // TODO: Create a proper result dialog for Avalonia
                var resultMessage = $"Result: {(result.Success ? "PASS" : "FAIL")}\nStatus: {result.StatusCode}\nBody: {result.ResponseBody}";
                // For now just log or show in console, can be enhanced later
                Console.WriteLine(resultMessage);
            }
        }
    }
}
