using System;

namespace WebAppointments.Models
{
    public class ExpectedPSFVisit
    {
        public int ParticipantId { get; set; }
        public int VisitSettingId { get; set; }
        public DateTime VisitDate { get; set; }
    }
}
