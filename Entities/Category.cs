namespace LifeOs.Entities
{
    public class Category : BaseEntity
    {
        public string Name { get; set; }
        public string Icon { get; set; }
        public string ColorHex { get; set; }
        public double XPMultiplier { get; set; }

        public ICollection<UserActivity> UserActivities { get; set; }
    }
}