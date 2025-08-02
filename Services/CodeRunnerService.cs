using ProgrammersToolKit.Services.Interfaces;
using System.Diagnostics;
using System.Threading.Tasks;

namespace ProgrammersToolKit.Services
{
    public class CodeRunnerService : ICodeRunnerService
    {
        public async Task<string> RunCodeAsync(string code, string language)
        {
            // This is a placeholder. In production, use sandboxing and security best practices!
            string fileName, args;
            switch (language.ToLower())
            {
                case "python":
                    fileName = "python";
                    args = $"-c \"{code.Replace("\"", "\\\"")}\"";
                    break;
                case "c#":
                    // For C#, you would need to use Roslyn or a similar tool
                    return "C# execution not implemented in this demo.";
                case "javascript":
                    fileName = "node";
                    args = $"-e \"{code.Replace("\"", "\\\"")}\"";
                    break;
                default:
                    return "Language not supported.";
            }
            var process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = fileName,
                    Arguments = args,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                }
            };
            process.Start();
            string output = await process.StandardOutput.ReadToEndAsync();
            string error = await process.StandardError.ReadToEndAsync();
            process.WaitForExit();
            return string.IsNullOrWhiteSpace(error) ? output : $"Error: {error}";
        }

        public async Task<string> RunCodeWithInputAsync(string code, string language, string input)
        {
            // This is a placeholder. In production, use sandboxing and security best practices!
            string fileName, args;
            switch (language.ToLower())
            {
                case "python":
                    fileName = "python";
                    args = $"-c \"{code.Replace("\"", "\\\"")}\"";
                    break;
                case "c#":
                    return "C# execution with input not implemented in this demo.";
                case "javascript":
                    fileName = "node";
                    args = $"-e \"{code.Replace("\"", "\\\"")}\"";
                    break;
                default:
                    return "Language not supported.";
            }
            var process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = fileName,
                    Arguments = args,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    RedirectStandardInput = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                }
            };
            process.Start();
            if (!string.IsNullOrEmpty(input))
            {
                await process.StandardInput.WriteLineAsync(input);
                process.StandardInput.Close();
            }
            string output = await process.StandardOutput.ReadToEndAsync();
            string error = await process.StandardError.ReadToEndAsync();
            process.WaitForExit();
            return string.IsNullOrWhiteSpace(error) ? output : $"Error: {error}";
        }
    }
}
