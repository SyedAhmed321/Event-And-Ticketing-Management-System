namespace Event___Ticketing_Management_System.Models.Dashboard
{
    public class RevenueAnalytics
    {
        public List<RevenueDataPoint> RevenueData { get; set; } = new();
    }

    public class RevenueDataPoint
    {
        public string Period { get; set; } = string.Empty;
        // e.g. "Jan 2026", "Week 1", etc.

        public decimal Revenue { get; set; }
    }
}
