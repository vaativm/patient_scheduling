using System;

namespace WebAppointments.Models
{
    public class MissedVisitsViewModel
    {
        public int Id { get; set; }
        public string StudyId { get; set; }
        public DateTime VisitDate { get; set; }
        public int VisitSettingId { get; set; }
        public string VisitStage { get; set; }
        public int ParticipantId { get; set; }
        public int ParticipantCame { get; set; }
        public DateTime? DateParticipantCame { get; set; }
        public DateTime? NextAppointment { get; set; }
    }
}
