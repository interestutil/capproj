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
            // Ensure we insert Order first to get its Id, then insert OrderLines with the correct OrderId.
            var lines = order.orderLines?.ToList() ?? new List<OrderLines>();

            // Detach lines from order to avoid EF trying to set FKs incorrectly
            order.orderLines = new List<OrderLines>();

            _context.orders.Add(order);
            await _context.SaveChangesAsync();

            if (lines.Any())
            {
                foreach (var l in lines)
                {
                    l.OrderId = order.Id;
                    // clear navigation to avoid cycles
                    l.order = null;
                    _context.orderLines.Add(l);
                }

                await _context.SaveChangesAsync();
            }
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
            // Load existing order including lines
            var existing = await _context.orders
                .Include(o => o.orderLines)
                .FirstOrDefaultAsync(o => o.Id == order.Id);
            if (existing == null) return;

            // Update scalar properties
            existing.Status = order.Status;
            existing.Type = order.Type;
            existing.CreatedAt = order.CreatedAt;

            // Handle order lines: add/update/delete
            var incoming = order.orderLines ?? new List<OrderLines>();

            // Delete removed lines
            var toRemove = existing.orderLines.Where(el => !incoming.Any(il => il.Id == el.Id)).ToList();
            foreach (var rem in toRemove)
            {
                _context.orderLines.Remove(rem);
            }

            // Update existing and add new
            foreach (var il in incoming)
            {
                if (il.Id == 0)
                {
                    il.OrderId = existing.Id;
                    _context.orderLines.Add(il);
                }
                else
                {
                    var ex = existing.orderLines.FirstOrDefault(x => x.Id == il.Id);
                    if (ex != null)
                    {
                        ex.ShelfId = il.ShelfId;
                        ex.Quantity = il.Quantity;
                    }
                }
            }

            await _context.SaveChangesAsync();
        }
    }
}
