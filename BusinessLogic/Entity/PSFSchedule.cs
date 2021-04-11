using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAppointments.BusinessLogic.Entity
{
    public class PSFSchedule
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int ParticipantId { get; set; }
        public string StudyId { get; set; }
        public int VisitStage { get; set; }
        public DateTime ExpectedDate { get; set; }
    }
}
