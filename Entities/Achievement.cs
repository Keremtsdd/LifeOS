namespace LifeOs.Entities
{
    public class Achievement
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string? IconUrl { get; set; }
        public int RequirementValue { get; set; }
    }
}