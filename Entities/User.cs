namespace LifeOs.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string IdentityId { get; set; }
        public string FullName { get; set; }
        public int Level { get; set; } = 1;
        public int TotalXP { get; set; } = 0;
        public int CurrentLevelXP { get; set; } = 0;
        public int NextLevelXP { get; set; } = 1000;
    }
}