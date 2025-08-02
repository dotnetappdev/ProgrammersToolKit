using ProgrammersToolKit.Core;
using ProgrammersToolKit.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammersToolKit.Services
{
    public class ApiTestRunner : IApiTestRunner
    {
        public async Task<ApiTestResult> RunTestAsync(ApiTestDefinition testDefinition)
        {
            var result = new ApiTestResult();
            try
            {
                using var client = new HttpClient();
                var request = new HttpRequestMessage(new HttpMethod(testDefinition.Method), testDefinition.Url);

                // Add headers
                if (!string.IsNullOrWhiteSpace(testDefinition.HeadersJson))
                {
                    try
                    {
                        var headers = System.Text.Json.JsonSerializer.Deserialize<Dictionary<string, string>>(testDefinition.HeadersJson);
                        if (headers != null)
                        {
                            foreach (var kv in headers)
                                request.Headers.TryAddWithoutValidation(kv.Key, kv.Value);
                        }
                    }
                    catch { /* ignore header parse errors */ }
                }

                // Add body
                if (!string.IsNullOrWhiteSpace(testDefinition.Body))
                {
                    request.Content = new StringContent(testDefinition.Body, System.Text.Encoding.UTF8,
                        testDefinition.BodyType?.ToLower() == "xml" ? "application/xml" : "application/json");
                }

                var response = await client.SendAsync(request);
                result.StatusCode = (int)response.StatusCode;
                result.ResponseHeaders = response.Headers.ToDictionary(h => h.Key, h => string.Join(", ", h.Value));
                result.ResponseBody = await response.Content.ReadAsStringAsync();
                result.Success = response.IsSuccessStatusCode;

                // Pretty print JSON/XML
                if (!string.IsNullOrWhiteSpace(result.ResponseBody))
                {
                    if (testDefinition.BodyType?.ToLower() == "json")
                    {
                        try
                        {
                            var jsonElem = System.Text.Json.JsonDocument.Parse(result.ResponseBody).RootElement;
                            result.ResponseBody = System.Text.Json.JsonSerializer.Serialize(jsonElem, new System.Text.Json.JsonSerializerOptions { WriteIndented = true });
                        }
                        catch { /* ignore pretty errors */ }
                    }
                    else if (testDefinition.BodyType?.ToLower() == "xml")
                    {
                        try
                        {
                            var doc = new System.Xml.XmlDocument();
                            doc.LoadXml(result.ResponseBody);
                            using var sw = new System.IO.StringWriter();
                            using var xw = new System.Xml.XmlTextWriter(sw) { Formatting = System.Xml.Formatting.Indented };
                            doc.WriteTo(xw);
                            xw.Flush();
                            result.ResponseBody = sw.ToString();
                        }
                        catch { /* ignore pretty errors */ }
                    }
                }
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
    }
}
