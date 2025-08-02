using ProgrammersToolKit.Core;
using ProgrammersToolKit.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace ProgrammersToolKit.Services
{
    public class ApiTestImportExportService : IApiTestImportExportService
    {
        public string ExportTestsToJson(IEnumerable<ApiTestDefinition> tests)
        {
            return JsonSerializer.Serialize(tests, new JsonSerializerOptions { WriteIndented = true });
        }

        public IEnumerable<ApiTestDefinition> ImportTestsFromJson(string json)
        {
            return JsonSerializer.Deserialize<List<ApiTestDefinition>>(json) ?? new List<ApiTestDefinition>();
        }

        public string ExportTestsToHttp(IEnumerable<ApiTestDefinition> tests)
        {
            var lines = new List<string>();
            foreach (var test in tests)
            {
                lines.Add($"### {test.Name}");
                lines.Add($"{test.Method} {test.Url}");
                if (!string.IsNullOrWhiteSpace(test.HeadersJson))
                {
                    try
                    {
                        var headers = JsonSerializer.Deserialize<Dictionary<string, string>>(test.HeadersJson);
                        if (headers != null)
                        {
                            foreach (var kv in headers)
                                lines.Add($"{kv.Key}: {kv.Value}");
                        }
                    }
                    catch { }
                }
                if (!string.IsNullOrWhiteSpace(test.Body))
                {
                    lines.Add("");
                    lines.Add(test.Body);
                }
                lines.Add("");
            }
            return string.Join("\n", lines);
        }

        public IEnumerable<ApiTestDefinition> ImportTestsFromHttp(string httpContent)
        {
            // Simple parser for .http format (one test per block)
            var tests = new List<ApiTestDefinition>();
            var blocks = httpContent.Split(new[] { "###" }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var block in blocks)
            {
                var lines = block.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
                if (lines.Length < 2) continue;
                var name = lines[0].Trim();
                var methodUrl = lines[1].Split(' ');
                if (methodUrl.Length < 2) continue;
                var method = methodUrl[0];
                var url = methodUrl[1];
                var headers = new Dictionary<string, string>();
                int i = 2;
                for (; i < lines.Length; i++)
                {
                    if (string.IsNullOrWhiteSpace(lines[i])) break;
                    var idx = lines[i].IndexOf(':');
                    if (idx > 0)
                        headers[lines[i].Substring(0, idx).Trim()] = lines[i].Substring(idx + 1).Trim();
                }
                var body = string.Join("\n", lines[(i + 1)..]);
                tests.Add(new ApiTestDefinition
                {
                    Name = name,
                    Url = url,
                    Method = method,
                    HeadersJson = JsonSerializer.Serialize(headers),
                    Body = body
                });
            }
            return tests;
        }
    }
}
