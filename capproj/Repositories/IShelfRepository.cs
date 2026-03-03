using capproj.Models;

namespace capproj.Repositories
{
    public interface IShelfRepository
    {
        Task<List<Shelf>> GetAllAsync();
        Task<Shelf?> GetByIdAsync(int id);
        Task AddAsync(Shelf shelf);
        Task UpdateAsync(Shelf shelf);
        Task DeleteAsync(int id);
    }
}
