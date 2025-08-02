using ProgrammersToolKit.Core;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ProgrammersToolKit.Data
{
    public class ApiTestGroupRepository : IApiTestGroupRepository
    {
        private readonly ApplicationDbContext _context;
        public ApiTestGroupRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddGroupAsync(ApiTestGroup group)
        {
            _context.ApiTestGroups.Add(group);
            await _context.SaveChangesAsync();
        }

        public async Task<List<ApiTestGroup>> GetAllGroupsAsync()
        {
            return await _context.ApiTestGroups.Include(g => g.Tests).ToListAsync();
        }

        public async Task<ApiTestGroup> GetGroupByIdAsync(int id)
        {
            return await _context.ApiTestGroups.Include(g => g.Tests).FirstOrDefaultAsync(g => g.Id == id);
        }

        public async Task UpdateGroupAsync(ApiTestGroup group)
        {
            _context.ApiTestGroups.Update(group);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteGroupAsync(int id)
        {
            var group = await _context.ApiTestGroups.FindAsync(id);
            if (group != null)
            {
                _context.ApiTestGroups.Remove(group);
                await _context.SaveChangesAsync();
            }
        }
    }
}
