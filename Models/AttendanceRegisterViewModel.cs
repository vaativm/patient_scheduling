namespace WebAppointments.Models
{
    public class AttendanceRegisterViewModel
    {
        public int VisitSettingId { get; set; }
        public string VisitStage { get; set; }
        public int Expected { get; set; }
        public int Atttended { get; set; }
        public decimal RetentionRate { get; set; }
    }
}
