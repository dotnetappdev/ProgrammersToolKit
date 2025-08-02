
using ProgrammersToolKit.Core;
using ProgrammersToolKit.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ProgrammersToolKit.Data
{
    public class ApiTestRepository : IApiTestRepository
    {
        private readonly ApplicationDbContext _context;
        public ApiTestRepository(ApplicationDbContext context)
        {
            _context = context;
            _context.Database.EnsureCreated();
        }

        public async Task AddTestAsync(ApiTestDefinition test)
        {
            _context.ApiTests.Add(test);
            await _context.SaveChangesAsync();
        }

        public async Task<List<ApiTestDefinition>> GetAllTestsAsync()
        {
            return await _context.ApiTests.ToListAsync();
        }

        public async Task<ApiTestDefinition> GetTestByIdAsync(int id)
        {
            return await _context.ApiTests.FindAsync(id);
        }

        public async Task UpdateTestAsync(ApiTestDefinition test)
        {
            _context.ApiTests.Update(test);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteTestAsync(int id)
        {
            var test = await _context.ApiTests.FindAsync(id);
            if (test != null)
            {
                _context.ApiTests.Remove(test);
                await _context.SaveChangesAsync();
            }
        }
    }
}
