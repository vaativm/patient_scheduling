using System;

namespace WebAppointments.BusinessLogic.Entity
{
    public class Schedule
    {
        public int Id { get; set; }
        public string studyId { get; set; }
        public int VisitStage { get; set; }
        public DateTime? WindowStart { get; set; }
        public DateTime? WindowEnd { get; set; }
    }
}
