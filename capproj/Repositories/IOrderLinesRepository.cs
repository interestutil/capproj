using capproj.Models;

namespace capproj.Repositories
{
    public interface IOrderLinesRepository
    {
        Task<List<OrderLines>> GetByOrderIdAsync(int orderId);
        Task<OrderLines?> GetByIdAsync(int id);
        Task AddAsync(OrderLines line);
        Task UpdateAsync(OrderLines line);
        Task DeleteAsync(int id);
    }
}
