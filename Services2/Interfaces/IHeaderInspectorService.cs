using System.Collections.Generic;

namespace ProgrammersToolKit.Services.Interfaces
{
    public interface IHeaderInspectorService
    {
        List<KeyValuePair<string, string>> ParseHeaders(string rawHeaders);
    }
}
