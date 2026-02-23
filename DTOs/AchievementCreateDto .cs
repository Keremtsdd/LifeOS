namespace LifeOs.DTOs
{
    public class AchievementCreateDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string IconUrl { get; set; }
        public int RequirementValue { get; set; }
    }
}