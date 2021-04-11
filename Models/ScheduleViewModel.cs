using System;

namespace WebAppointments.Models
{
    public class ScheduleViewModel
    {
        public DateTime? ExpectedVisitDate { get; set; }
        public DateTime? NextAppointmentDate { get; set; }
    }
}
