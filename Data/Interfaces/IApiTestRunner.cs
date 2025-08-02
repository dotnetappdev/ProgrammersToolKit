using System.Threading.Tasks;
using ProgrammersToolKit.Core;

namespace ProgrammersToolKit.Data.Interfaces
{
    public interface IApiTestRunner
    {
        Task<ApiTestResult> RunTestAsync(ApiTestDefinition testDefinition);
    }
}
