namespace LifeOs.Entities
{
    public class UserAchievement
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int AchievementId { get; set; }
        public DateTime EarnedDate { get; set; }
    }
}