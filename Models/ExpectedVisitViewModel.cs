using System;

namespace WebAppointments.Models
{
    public class ExpectedVisitViewModel
    {
        public DateTime VisitDate { get; set; }
        public int VisitSettingId { get; set; }
        public string VisitStage { get; set; }
        public int ParticipantId { get; set; }
        public DateTime? DateParticipantCame { get; set; }
    }
}
