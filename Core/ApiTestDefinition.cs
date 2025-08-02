using System;
using System.Collections.Generic;

namespace ProgrammersToolKit.Core
{
    public class ApiTestDefinition
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Url { get; set; } = string.Empty;
        public string Method { get; set; } = "GET";
        public string Body { get; set; } = string.Empty;
        public string BodyType { get; set; } = "JSON";
        public Dictionary<string, string> Headers { get; set; } = new();
        public string HeadersJson { get; set; } = "{}";
        public List<string> Assertions { get; set; } = new();
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public DateTime TestDate { get; set; } = DateTime.UtcNow;
    }
}