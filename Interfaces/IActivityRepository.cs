using LifeOs.Entities;

namespace LifeOs.Interfaces
{
    public interface IActivityRepository
    {
        Task AddAsync(UserActivity activity);
    }
}