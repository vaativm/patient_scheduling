using System;

namespace WebAppointments.Models
{
    public class AttendanceViewModel
    {
        public int ParticipantId { get; set; }
        public DateTime VisitDate { get; set; }
        public int VisitSettingId { get; set; }
    }
}
