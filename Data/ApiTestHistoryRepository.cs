using ProgrammersToolKit.Core;
using ProgrammersToolKit.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ProgrammersToolKit.Data
{
    public class ApiTestHistoryRepository : IApiTestHistoryRepository
    {
        private readonly ApplicationDbContext _context;
        public ApiTestHistoryRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddHistoryAsync(ApiTestHistoryEntry entry)
        {
            _context.ApiTestHistory.Add(entry);
            await _context.SaveChangesAsync();
        }

        public async Task<List<ApiTestHistoryEntry>> GetHistoryForTestAsync(int apiTestDefinitionId)
        {
            return await _context.ApiTestHistory.Where(h => h.ApiTestDefinitionId == apiTestDefinitionId).ToListAsync();
        }

        public async Task<List<ApiTestHistoryEntry>> GetAllHistoryAsync()
        {
            return await _context.ApiTestHistory.ToListAsync();
        }
    }
}
