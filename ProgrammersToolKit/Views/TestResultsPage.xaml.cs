using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using ProgrammersToolKit.Core;
using ProgrammersToolKit.Services.Interfaces;
using System.Collections.Generic;

namespace ProgrammersToolKit.Views
{
    public sealed partial class TestResultsPage : Page
    {
        private readonly IApiTestRepository _repo;
        public TestResultsPage()
        {
            this.InitializeComponent();
            _repo = (IApiTestRepository)AppHost.ServiceProvider.GetService(typeof(IApiTestRepository));
            LoadResults();
        }

        private async void LoadResults()
        {
            TestResultsListView.ItemsSource = await _repo.GetAllTestsAsync();
        }
    }
}
