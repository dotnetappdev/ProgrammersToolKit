using ProgrammersToolKit.Core;
using ProgrammersToolKit.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text.Json;

namespace ProgrammersToolKit.Services
{
    public class ApiTestAssertionService : IApiTestAssertionService
    {
        public List<ApiTestAssertionResult> EvaluateAssertions(ApiTestResult result, string assertionsScript)
        {
            var results = new List<ApiTestAssertionResult>();
            if (string.IsNullOrWhiteSpace(assertionsScript)) return results;
            var lines = assertionsScript.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var line in lines)
            {
                var assertion = line.Trim();
                if (assertion.StartsWith("Assert.AreEqual(", StringComparison.OrdinalIgnoreCase))
                {
                    // Example: Assert.AreEqual(200, StatusCode)
                    try
                    {
                        var args = assertion.Substring(16, assertion.Length - 17).Split(',');
                        var expected = args[0].Trim();
                        var actual = args[1].Trim();
                        if (actual.Equals("StatusCode", StringComparison.OrdinalIgnoreCase))
                        {
                            var pass = result.StatusCode.ToString() == expected;
                            results.Add(new ApiTestAssertionResult { Assertion = assertion, Passed = pass, Message = pass ? "PASS" : $"Expected {expected}, got {result.StatusCode}" });
                        }
                        else if (actual.Equals("Success", StringComparison.OrdinalIgnoreCase))
                        {
                            var pass = result.Success.ToString().ToLower() == expected.ToLower();
                            results.Add(new ApiTestAssertionResult { Assertion = assertion, Passed = pass, Message = pass ? "PASS" : $"Expected {expected}, got {result.Success}" });
                        }
                        else if (actual.StartsWith("BodyContains", StringComparison.OrdinalIgnoreCase))
                        {
                            var val = expected.Trim('"');
                            var pass = result.ResponseBody != null && result.ResponseBody.Contains(val);
                            results.Add(new ApiTestAssertionResult { Assertion = assertion, Passed = pass, Message = pass ? "PASS" : $"Body does not contain '{val}'" });
                        }
                        else
                        {
                            results.Add(new ApiTestAssertionResult { Assertion = assertion, Passed = false, Message = "Unknown assertion target." });
                        }
                    }
                    catch (Exception ex)
                    {
                        results.Add(new ApiTestAssertionResult { Assertion = assertion, Passed = false, Message = $"Error: {ex.Message}" });
                    }
                }
                else
                {
                    results.Add(new ApiTestAssertionResult { Assertion = assertion, Passed = false, Message = "Unknown assertion format." });
                }
            }
            return results;
        }
    }
}
