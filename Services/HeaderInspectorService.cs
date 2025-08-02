using ProgrammersToolKit.Services.Interfaces;
using System;
using System.Collections.Generic;

namespace ProgrammersToolKit.Services
{
    public class HeaderInspectorService : IHeaderInspectorService
    {
        public List<KeyValuePair<string, string>> ParseHeaders(string rawHeaders)
        {
            var lines = rawHeaders.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            var headers = new List<KeyValuePair<string, string>>();
            foreach (var line in lines)
            {
                var idx = line.IndexOf(':');
                if (idx > 0)
                {
                    var key = line.Substring(0, idx).Trim();
                    var value = line.Substring(idx + 1).Trim();
                    headers.Add(new KeyValuePair<string, string>(key, value));
                }
            }
            return headers;
        }
    }
}
