using System;
using System.Threading.Tasks;

namespace ProgrammersToolKit.Core
{
    public interface IApiTestRunner
    {
        Task<ApiTestResult> RunTestAsync(ApiTestDefinition testDefinition);
    }

    public class ApiTestDefinition
    {
        public int Id { get; set; } // EF Core primary key
        public string Name { get; set; }
        public string Url { get; set; }
        public string Method { get; set; }
        public string HeadersJson { get; set; }
        public string Body { get; set; }
        public string BodyType { get; set; } // JSON, XML, etc.
        public string Assertions { get; set; }
        public DateTime TestDate { get; set; }
    }

    public class ApiTestResult
    {
        public bool Success { get; set; }
        public int StatusCode { get; set; } // HTTP status code
        public string ResponseBody { get; set; }
        public string ResponseHeaders { get; set; }
        public string ErrorMessage { get; set; }
    }
}
