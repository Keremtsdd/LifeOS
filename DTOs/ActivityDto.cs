namespace LifeOs.DTOs
{
    public class ActivityDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int DurationMinutes { get; set; }
        public int EarnedXP { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CategoryName { get; set; }
    }
}