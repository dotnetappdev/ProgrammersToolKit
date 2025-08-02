using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Input;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProgrammersToolKit.Controls
{
    public partial class SimpleCodeEditor : UserControl
    {
        private readonly List<string> _terminalHistory = new List<string>();
        private int _historyIndex = -1;
        private string _currentDirectory = Environment.CurrentDirectory;

        public SimpleCodeEditor()
        {
            this.InitializeComponent();
            EditorBox.TextChanged += EditorBox_TextChanged;
            TerminalOutputBlock.Text = $"Welcome to the Debug Terminal. Type commands below.\nCurrent directory: {_currentDirectory}\n";
            StatusBarText.Text = "Ready";
            TerminalInputBox.KeyDown += TerminalInputBox_KeyDown;
        }

        // Handle Copy All button click
        private void TerminalCopyAllButton_Click(object sender, RoutedEventArgs e)
        {
            CopyTerminalOutputToClipboard();
        }
        // (Removed duplicate constructor)

        public string Text
        {
            get
            {
                EditorBox.Document.GetText(Windows.UI.Text.TextGetOptions.None, out var text);
                return text;
            }
            set
            {
                EditorBox.Document.SetText(Windows.UI.Text.TextSetOptions.None, value);
            }
        }

        // Append output to the terminal window
        public void AppendTerminalOutput(string text)
        {
            TerminalOutputBlock.Text += text + "\n";
        }

        // Copy all terminal output to clipboard
        public void CopyTerminalOutputToClipboard()
        {
            var dataPackage = new Windows.ApplicationModel.DataTransfer.DataPackage();
            dataPackage.SetText(TerminalOutputBlock.Text);
            Windows.ApplicationModel.DataTransfer.Clipboard.SetContent(dataPackage);
            SetStatus("Terminal output copied to clipboard");
        }

        // Set status bar text
        public void SetStatus(string status)
        {
            StatusBarText.Text = status;
        }

        // Handle Enter key and Up/Down for history in terminal input
        private void TerminalInputBox_KeyDown(object sender, Avalonia.Input.KeyEventArgs e)
        {
            if (e.Key == Avalonia.Input.Key.Enter)
            {
                HandleTerminalCommand();
                e.Handled = true;
            }
            else if (e.Key == Avalonia.Input.Key.Up)
            {
                if (_terminalHistory.Count > 0 && _historyIndex < _terminalHistory.Count - 1)
                {
                    _historyIndex++;
                    TerminalInputBox.Text = _terminalHistory[_terminalHistory.Count - 1 - _historyIndex];
                    TerminalInputBox.SelectionStart = TerminalInputBox.Text.Length;
                }
                e.Handled = true;
            }
            else if (e.Key == Avalonia.Input.Key.Down)
            {
                if (_historyIndex > 0)
                {
                    _historyIndex--;
                    TerminalInputBox.Text = _terminalHistory[_terminalHistory.Count - 1 - _historyIndex];
                    TerminalInputBox.SelectionStart = TerminalInputBox.Text.Length;
                }
                else if (_historyIndex == 0)
                {
                    _historyIndex = -1;
                    TerminalInputBox.Text = string.Empty;
                }
                e.Handled = true;
            }
        }

        // Handle Send button click
        private void TerminalSendButton_Click(object sender, RoutedEventArgs e)
        {
            HandleTerminalCommand();
        }

        // Process terminal command
        private void HandleTerminalCommand()
        {
            var cmd = TerminalInputBox.Text.Trim();
            if (string.IsNullOrEmpty(cmd)) return;
            AppendTerminalOutput($"> {cmd}");
            _terminalHistory.Add(cmd);
            _historyIndex = -1;
            TerminalInputBox.Text = string.Empty;
            // Built-in commands
            if (cmd.Equals("clear", StringComparison.OrdinalIgnoreCase))
            {
                TerminalOutputBlock.Text = string.Empty;
                SetStatus("Terminal cleared");
                return;
            }
            if (cmd.Equals("help", StringComparison.OrdinalIgnoreCase))
            {
                AppendTerminalOutput("Available commands: clear, help, echo <text>, pwd, cd <dir>, ls, copyall");
                SetStatus("Help shown");
                return;
            }
            if (cmd.StartsWith("echo ", StringComparison.OrdinalIgnoreCase))
            {
                AppendTerminalOutput(cmd.Substring(5));
                SetStatus("Echoed");
                return;
            }
            if (cmd.Equals("pwd", StringComparison.OrdinalIgnoreCase))
            {
                AppendTerminalOutput(_currentDirectory);
                SetStatus("Current directory shown");
                return;
            }
            if (cmd.StartsWith("cd ", StringComparison.OrdinalIgnoreCase))
            {
                var dir = cmd.Substring(3).Trim();
                try
                {
                    var newDir = System.IO.Path.GetFullPath(System.IO.Path.Combine(_currentDirectory, dir));
                    if (System.IO.Directory.Exists(newDir))
                    {
                        _currentDirectory = newDir;
                        AppendTerminalOutput($"Changed directory to: {_currentDirectory}");
                        SetStatus($"Directory: {_currentDirectory}");
                    }
                    else
                    {
                        AppendTerminalOutput($"Directory not found: {dir}");
                        SetStatus("Directory not found");
                    }
                }
                catch (Exception ex)
                {
                    AppendTerminalOutput($"Error: {ex.Message}");
                    SetStatus("Error changing directory");
                }
                return;
            }
            if (cmd.Equals("ls", StringComparison.OrdinalIgnoreCase))
            {
                try
                {
                    var files = System.IO.Directory.GetFileSystemEntries(_currentDirectory);
                    foreach (var f in files)
                        AppendTerminalOutput(f);
                    SetStatus("Directory listed");
                }
                catch (Exception ex)
                {
                    AppendTerminalOutput($"Error: {ex.Message}");
                    SetStatus("Error listing directory");
                }
                return;
            }
            if (cmd.Equals("copyall", StringComparison.OrdinalIgnoreCase))
            {
                CopyTerminalOutputToClipboard();
                return;
            }
            // TODO: Integrate with backend for real shell commands if desired
            AppendTerminalOutput($"Unknown command: {cmd}");
            SetStatus("Unknown command");
        }

        private void EditorBox_TextChanged(object sender, RoutedEventArgs e)
        {
            UpdateLineNumbers();
        }

        private void UpdateLineNumbers()
        {
            // TODO: Implement for Avalonia - temporarily disabled to get basic build working
            /*
            var text = EditorBox.Text ?? "";
            var lines = text.Split('\n');
            LineNumbersPanel.Children.Clear();
            for (int i = 1; i <= lines.Length; i++)
            {
                var tb = new TextBlock { Text = i.ToString(), FontFamily = "Consolas", FontSize = 14 };
                LineNumbersPanel.Children.Add(tb);
            }
            */
        }
    }
}
