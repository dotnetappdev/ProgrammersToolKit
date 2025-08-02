using ProgrammersToolKit.Core;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProgrammersToolKit.Data
{
    public interface IApiTestHistoryRepository
    {
        Task AddHistoryAsync(ApiTestHistoryEntry entry);
        Task<List<ApiTestHistoryEntry>> GetHistoryForTestAsync(int apiTestDefinitionId);
        Task<List<ApiTestHistoryEntry>> GetAllHistoryAsync();
    }
}
