using System.Collections.Generic;

namespace ProgrammersToolKit.Services.Interfaces
{
    public interface ICookieInspectorService
    {
        List<KeyValuePair<string, string>> ParseCookie(string rawCookie);
    }
}
