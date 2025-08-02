using ProgrammersToolKit.Core;
using System.Collections.Generic;

namespace ProgrammersToolKit.Services.Interfaces
{
    public interface IApiTestAssertionService
    {
        List<ApiTestAssertionResult> EvaluateAssertions(ApiTestResult result, string assertionsScript);
    }

    public class ApiTestAssertionResult
    {
        public string Assertion { get; set; }
        public bool Passed { get; set; }
        public string Message { get; set; }
    }
}
