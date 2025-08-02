using ProgrammersToolKit.Services.Interfaces;
using System;
using System.Collections.Generic;

namespace ProgrammersToolKit.Services
{
    public class CookieInspectorService : ICookieInspectorService
    {
        public List<KeyValuePair<string, string>> ParseCookie(string rawCookie)
        {
            var pairs = rawCookie.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
            var result = new List<KeyValuePair<string, string>>();
            foreach (var pair in pairs)
            {
                var idx = pair.IndexOf('=');
                if (idx > 0)
                {
                    var key = pair.Substring(0, idx).Trim();
                    var value = pair.Substring(idx + 1).Trim();
                    result.Add(new KeyValuePair<string, string>(key, value));
                }
            }
            return result;
        }
    }
}
