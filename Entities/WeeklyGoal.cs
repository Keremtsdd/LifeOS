namespace LifeOs.Entities
{
    public class WeeklyGoal
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public virtual Category Category { get; set; }
        public int CategoryId { get; set; }
        public int TargetMinutes { get; set; }
        public DateTime StartDate { get; set; }
    }
}