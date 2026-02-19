namespace LifeOs.DTOs
{
    public class DashboardSummaryDto
    {
        public int TotalXP { get; set; }
        public int Level { get; set; }
        public List<CategoryProgressDto> CategoryScores { get; set; }
    }
}