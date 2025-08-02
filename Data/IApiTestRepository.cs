
using ProgrammersToolKit.Core;
using ProgrammersToolKit.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProgrammersToolKit.Data
{
    public interface IApiTestRepository
    {
        Task AddTestAsync(ApiTestDefinition test);
        Task<List<ApiTestDefinition>> GetAllTestsAsync();
        Task<ApiTestDefinition> GetTestByIdAsync(int id);
        Task UpdateTestAsync(ApiTestDefinition test);
        Task DeleteTestAsync(int id);
    }
}
