using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using capproj.Data;
using capproj.Models;
using Microsoft.EntityFrameworkCore;

namespace capproj.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly Context _context;
        public OrderRepository(Context context) => _context = context;

        public async Task AddAsync(Order order)
        {
            _context.orders.Add(order);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.orders.FindAsync(id);
            if (entity == null) return;
            _context.orders.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Order>> GetAllAsync()
        {
            return await _context.orders
                .Include(o => o.orderLines)
                    .ThenInclude(ol => ol.shelf)
                .ToListAsync();
        }

        public async Task<Order?> GetByIdAsync(int id)
        {
            return await _context.orders
                .Include(o => o.orderLines)
                    .ThenInclude(ol => ol.shelf)
                .FirstOrDefaultAsync(o => o.Id == id);
        }

        public async Task UpdateAsync(Order order)
        {
            _context.orders.Update(order);
            await _context.SaveChangesAsync();
        }
    }
}
