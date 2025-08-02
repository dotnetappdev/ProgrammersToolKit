using System;
using System.Collections.Generic;

namespace ProgrammersToolKit.Core
{
    public class ApiTestResult
    {
        public int StatusCode { get; set; }
        public string ResponseBody { get; set; } = string.Empty;
        public Dictionary<string, string> ResponseHeaders { get; set; } = new();
        public TimeSpan Duration { get; set; }
        public bool Success { get; set; }
        public string ErrorMessage { get; set; } = string.Empty;
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    }
}