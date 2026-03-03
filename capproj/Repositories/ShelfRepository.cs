using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using capproj.Data;
using capproj.Models;
using Microsoft.EntityFrameworkCore;

namespace capproj.Repositories
{
    public class ShelfRepository : IShelfRepository
    {
        private readonly Context _context;
        public ShelfRepository(Context context) => _context = context;

        public async Task AddAsync(Shelf shelf)
        {
            _context.shelves.Add(shelf);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.shelves.FindAsync(id);
            if (entity == null) return;
            _context.shelves.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Shelf>> GetAllAsync()
        {
            return await _context.shelves.ToListAsync();
        }

        public async Task<Shelf?> GetByIdAsync(int id)
        {
            return await _context.shelves.FindAsync(id);
        }

        public async Task UpdateAsync(Shelf shelf)
        {
            _context.shelves.Update(shelf);
            await _context.SaveChangesAsync();
        }
    }
}
