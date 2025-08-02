using ProgrammersToolKit.Core;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProgrammersToolKit.Data
{
    public interface IApiTestGroupRepository
    {
        Task AddGroupAsync(ApiTestGroup group);
        Task<List<ApiTestGroup>> GetAllGroupsAsync();
        Task<ApiTestGroup> GetGroupByIdAsync(int id);
        Task UpdateGroupAsync(ApiTestGroup group);
        Task DeleteGroupAsync(int id);
    }
}
