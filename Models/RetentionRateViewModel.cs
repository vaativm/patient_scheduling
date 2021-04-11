using System;

namespace WebAppointments.Models
{
    public class RetentionRateViewModel
    {
        public int Year { get; set; }
        public int Week { get; set; }
        public string VisitType { get; set; }
        public int VisitSettingId { get; set; }
        public int Expected { get; set; }
        public int Attended { get; set; }
        public decimal Percentage { get; set; }
    }

    public class ExpectedVisitsViewModel
    {
        public int VisitSettingId { get; set; }
        public int ParticipantId { get; set; }
        public DateTime ExpectedDate { get; set; }
    }
    public class VisitViewModel
    {
        public string VisitType { get; set; }
        public int VisitStage { get; set; }
        public int ParticipantId { get; set; }
        public DateTime VisitDate { get; set; }
        public DateTime NextAppointment { get; set; }
        public int ParticipantCame { get; set; }
    }
}
