using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using capproj.Data;
using capproj.Models;
using Microsoft.EntityFrameworkCore;

namespace capproj.Repositories
{
    public class OrderLinesRepository : IOrderLinesRepository
    {
        private readonly Context _context;
        public OrderLinesRepository(Context context) => _context = context;

        public async Task AddAsync(OrderLines line)
        {
            _context.orderLines.Add(line);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.orderLines.FindAsync(id);
            if (entity == null) return;
            _context.orderLines.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<OrderLines?> GetByIdAsync(int id)
        {
            return await _context.orderLines
                .Include(ol => ol.shelf)
                .Include(ol => ol.order)
                .FirstOrDefaultAsync(ol => ol.Id == id);
        }

        public async Task<List<OrderLines>> GetByOrderIdAsync(int orderId)
        {
            return await _context.orderLines
                .Where(ol => ol.OrderId == orderId)
                .Include(ol => ol.shelf)
                .ToListAsync();
        }

        public async Task UpdateAsync(OrderLines line)
        {
            _context.orderLines.Update(line);
            await _context.SaveChangesAsync();
        }
    }
}
