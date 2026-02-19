using LifeOs.Entities;
using LifeOs.Interfaces;
using LifeOs.Context;

namespace LifeOs.Repositories
{
    public class ActivityRepository : IActivityRepository
    {
        private readonly AppDbContext _context;
        public ActivityRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(UserActivity activity)
        {
            await _context.UserActivities.AddAsync(activity);
        }
    }
}