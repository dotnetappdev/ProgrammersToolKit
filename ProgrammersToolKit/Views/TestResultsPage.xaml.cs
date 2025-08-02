using Avalonia.Interactivity;
using Avalonia.Controls;
using Avalonia.Controls;
using ProgrammersToolKit.Core;
using ProgrammersToolKit.Services.Interfaces;
using System.Collections.Generic;

namespace ProgrammersToolKit.Views
{
    public partial class TestResultsPage : UserControl
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
