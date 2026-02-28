namespace LifeOs.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string IdentityId { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string? ProfilePictureUrl { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public bool IsActive { get; set; } = true;
        public int Level { get; set; } = 1;
        public int TotalXP { get; set; } = 0;
        public int CurrentLevelXP { get; set; } = 0;
        public int NextLevelXP { get; set; } = 1000;
        public virtual ICollection<UserActivity> Activities { get; set; } = new List<UserActivity>();
    }
}