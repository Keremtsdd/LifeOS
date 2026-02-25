namespace LifeOs.DTOs
{
    public class UserStatsDto
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public int TotalXP { get; set; }
        public int Level { get; set; }
        public int NextLevelXP { get; set; }
        public int ActivityCount { get; set; }
        public string LastActivity { get; set; }
        public List<RecentActivityDto> RecentActivities { get; set; } = new();
        public List<WeeklyChartDto> WeeklyChart { get; set; } = new();
    }
}