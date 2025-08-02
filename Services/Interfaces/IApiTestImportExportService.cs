using ProgrammersToolKit.Core;
using System.Collections.Generic;

namespace ProgrammersToolKit.Services.Interfaces
{
    public interface IApiTestImportExportService
    {
        string ExportTestsToJson(IEnumerable<ApiTestDefinition> tests);
        IEnumerable<ApiTestDefinition> ImportTestsFromJson(string json);
        string ExportTestsToHttp(IEnumerable<ApiTestDefinition> tests);
        IEnumerable<ApiTestDefinition> ImportTestsFromHttp(string httpContent);
    }
}
