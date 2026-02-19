namespace LifeOs.Entities
{
    public class UserActivity : BaseEntity
    {
        public string UserId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int DurationMinutes { get; set; }
        public int EarnedXP { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}