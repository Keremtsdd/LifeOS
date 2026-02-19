using LifeOs.Entities;

namespace LifeOs.Interfaces
{
    public interface ICategoryRepository
    {
        Task<Category> GetByIdAsync(int id);
    }
}