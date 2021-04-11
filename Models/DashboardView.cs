namespace WebAppointments.Models
{
    public class DashboardView
    {
        public int Participants { get; set; }
        public int Visits { get; set; }
        public int Missed { get; set; }
        public int Schedulled { get; set; }
        public int Overdue { get; set; }
    }
}