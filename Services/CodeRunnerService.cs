using ProgrammersToolKit.Services.Interfaces;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Emit;
using System.Diagnostics;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammersToolKit.Services
{
    public class CodeRunnerService : ICodeRunnerService
    {
        private const int TimeoutMs = 30000; // 30 seconds timeout

        public async Task<string> RunCodeAsync(string code, string language)
        {
            return await RunCodeWithInputAsync(code, language, "");
        }

        public async Task<string> RunCodeWithInputAsync(string code, string language, string input)
        {
            try
            {
                switch (language.ToLower())
                {
                    case "csharp":
                    case "c#":
                        return await RunCSharpCodeAsync(code, input);
                    case "python":
                        return await RunPythonCodeAsync(code, input);
                    case "javascript":
                        return await RunJavaScriptCodeAsync(code, input);
                    default:
                        return "Language not supported. Supported languages: C#, Python, JavaScript";
                }
            }
            catch (Exception ex)
            {
                return $"Error executing code: {ex.Message}";
            }
        }

        private async Task<string> RunCSharpCodeAsync(string code, string input)
        {
            try
            {
                // Wrap the code in a proper class structure if it doesn't have one
                var wrappedCode = WrapCSharpCode(code);

                var syntaxTree = CSharpSyntaxTree.ParseText(wrappedCode);
                
                var references = new[]
                {
                    MetadataReference.CreateFromFile(typeof(object).Assembly.Location),
                    MetadataReference.CreateFromFile(typeof(Console).Assembly.Location),
                    MetadataReference.CreateFromFile(typeof(System.Runtime.AssemblyTargetedPatchBandAttribute).Assembly.Location),
                    MetadataReference.CreateFromFile(Assembly.Load("System.Runtime").Location)
                };

                var compilation = CSharpCompilation.Create(
                    "DynamicCode",
                    new[] { syntaxTree },
                    references,
                    new CSharpCompilationOptions(OutputKind.ConsoleApplication));

                using var ms = new MemoryStream();
                EmitResult result = compilation.Emit(ms);

                if (!result.Success)
                {
                    var failures = result.Diagnostics.Where(diagnostic =>
                        diagnostic.IsWarningAsError ||
                        diagnostic.Severity == DiagnosticSeverity.Error);

                    var errors = string.Join("\n", failures.Select(f => f.GetMessage()));
                    return $"Compilation errors:\n{errors}";
                }

                ms.Seek(0, SeekOrigin.Begin);
                var assembly = Assembly.Load(ms.ToArray());
                var type = assembly.GetType("DynamicProgram");
                var method = type?.GetMethod("Main", BindingFlags.Static | BindingFlags.Public);

                if (method == null)
                {
                    return "Error: No Main method found";
                }

                // Capture console output
                var originalOut = Console.Out;
                var originalIn = Console.In;
                
                using var outputWriter = new StringWriter();
                using var inputReader = new StringReader(input ?? "");
                
                Console.SetOut(outputWriter);
                Console.SetIn(inputReader);

                try
                {
                    var task = Task.Run(() =>
                    {
                        if (method.GetParameters().Length == 0)
                        {
                            method.Invoke(null, null);
                        }
                        else
                        {
                            method.Invoke(null, new object[] { new string[0] });
                        }
                    });

                    if (await Task.WhenAny(task, Task.Delay(TimeoutMs)) == task)
                    {
                        await task; // Propagate exceptions
                        return outputWriter.ToString();
                    }
                    else
                    {
                        return "Error: Code execution timed out (30 seconds)";
                    }
                }
                finally
                {
                    Console.SetOut(originalOut);
                    Console.SetIn(originalIn);
                }
            }
            catch (Exception ex)
            {
                return $"Runtime error: {ex.Message}";
            }
        }

        private string WrapCSharpCode(string code)
        {
            // Check if code already has a Main method
            if (code.Contains("static void Main") || code.Contains("static async Task Main"))
            {
                // If it has Main but no class, wrap it in a class
                if (!code.Contains("class ") && !code.Contains("public class"))
                {
                    return $@"
using System;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;

public class DynamicProgram
{{
    {code}
}}";
                }
                else
                {
                    // Replace class name with DynamicProgram
                    return code.Replace("class Program", "class DynamicProgram")
                              .Replace("public class Program", "public class DynamicProgram");
                }
            }
            else
            {
                // Wrap statements in a Main method
                return $@"
using System;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;

public class DynamicProgram
{{
    public static void Main()
    {{
        {code}
    }}
}}";
            }
        }

        private async Task<string> RunPythonCodeAsync(string code, string input)
        {
            return await RunExternalProcessAsync("python3", $"-c \"{EscapeForShell(code)}\"", input);
        }

        private async Task<string> RunJavaScriptCodeAsync(string code, string input)
        {
            return await RunExternalProcessAsync("node", $"-e \"{EscapeForShell(code)}\"", input);
        }

        private async Task<string> RunExternalProcessAsync(string fileName, string arguments, string input)
        {
            try
            {
                var process = new Process
                {
                    StartInfo = new ProcessStartInfo
                    {
                        FileName = fileName,
                        Arguments = arguments,
                        RedirectStandardOutput = true,
                        RedirectStandardError = true,
                        RedirectStandardInput = true,
                        UseShellExecute = false,
                        CreateNoWindow = true
                    }
                };

                var outputBuilder = new StringBuilder();
                var errorBuilder = new StringBuilder();

                process.OutputDataReceived += (sender, e) =>
                {
                    if (e.Data != null)
                        outputBuilder.AppendLine(e.Data);
                };

                process.ErrorDataReceived += (sender, e) =>
                {
                    if (e.Data != null)
                        errorBuilder.AppendLine(e.Data);
                };

                process.Start();
                process.BeginOutputReadLine();
                process.BeginErrorReadLine();

                if (!string.IsNullOrEmpty(input))
                {
                    await process.StandardInput.WriteLineAsync(input);
                    process.StandardInput.Close();
                }

                bool finished = process.WaitForExit(TimeoutMs);
                
                if (!finished)
                {
                    process.Kill();
                    return "Error: Code execution timed out (30 seconds)";
                }

                string output = outputBuilder.ToString();
                string error = errorBuilder.ToString();

                if (!string.IsNullOrWhiteSpace(error))
                {
                    return $"Error: {error}\n\nOutput: {output}";
                }

                return string.IsNullOrWhiteSpace(output) ? "(No output)" : output;
            }
            catch (Exception ex)
            {
                return $"Error starting {fileName}: {ex.Message}. Make sure {fileName} is installed and available in PATH.";
            }
        }

        private string EscapeForShell(string text)
        {
            return text.Replace("\"", "\\\"").Replace("\n", "\\n").Replace("\r", "");
        }
    }
}
