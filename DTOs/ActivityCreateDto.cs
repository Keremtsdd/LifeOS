namespace LifeOs.DTOs
{
    public class ActivityCreateDto
    {
        public string Title { get; set; }
        public int DurationMinutes { get; set; }
        public int CategoryId { get; set; }
        public string UserId { get; set; }
    }
}