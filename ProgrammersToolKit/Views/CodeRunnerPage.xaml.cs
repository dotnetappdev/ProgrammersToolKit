using Avalonia.Interactivity;
using Avalonia.Controls;
using Avalonia.Controls;
using ProgrammersToolKit.Services.Interfaces;

namespace ProgrammersToolKit.Views
{
    public partial class CodeRunnerPage : UserControl
    {
        private readonly ICodeRunnerService _service;
        private readonly ObservableCollection<string> _testCases = new ObservableCollection<string>();
        private readonly ObservableCollection<string> _testCaseResults = new ObservableCollection<string>();
        public CodeRunnerPage()
        {
            this.InitializeComponent();
            _service = (ICodeRunnerService)AppHost.ServiceProvider.GetService(typeof(ICodeRunnerService));
            TestCaseResultsListView.ItemsSource = _testCaseResults;
        }

        private async void RunCode_Click(object sender, RoutedEventArgs e)
        {
            var code = CodeInputBox.Text ?? string.Empty;
            ProgrammersToolKit.Controls.SimpleCodeEditor editor = null;
            if (CodeInputBox is ProgrammersToolKit.Controls.SimpleCodeEditor ed)
            {
                editor = ed;
                code = ed.Text ?? string.Empty;
            }
            var language = (LanguageBox.SelectedItem as ComboBoxItem)?.Content?.ToString();
            if (string.IsNullOrWhiteSpace(language)) return;
            CodeOutputBox.Text = "Running...";
            editor?.SetStatus($"Running {language} code...");
            var output = await _service.RunCodeAsync(code, language);
            CodeOutputBox.Text = output;
            editor?.AppendTerminalOutput($"[Run] Output:\n{output}");
            editor?.SetStatus("Execution complete");
        }

        private void AddTestCaseButton_Click(object sender, RoutedEventArgs e)
        {
            var input = NewTestCaseTextBox.Text;
            if (!string.IsNullOrWhiteSpace(input))
            {
                _testCases.Add(input);
                NewTestCaseTextBox.Text = string.Empty;
            }
        }

        private async void RunAllTestCasesButton_Click(object sender, RoutedEventArgs e)
        {
            var code = CodeInputBox.Text ?? string.Empty;
            ProgrammersToolKit.Controls.SimpleCodeEditor editor = null;
            if (CodeInputBox is ProgrammersToolKit.Controls.SimpleCodeEditor ed)
            {
                editor = ed;
                code = ed.Text ?? string.Empty;
            }
            var language = (LanguageBox.SelectedItem as ComboBoxItem)?.Content?.ToString();
            if (string.IsNullOrWhiteSpace(language)) return;
            _testCaseResults.Clear();
            editor?.SetStatus($"Running all test cases...");
            foreach (var testCase in _testCases)
            {
                var result = await _service.RunCodeWithInputAsync(code, language, testCase);
                _testCaseResults.Add($"Input: {testCase}\nOutput: {result}");
                editor?.AppendTerminalOutput($"[TestCase] Input: {testCase}\nOutput: {result}");
            }
            editor?.SetStatus("Test cases complete");
        }
    }
}
