using System;
using System.ComponentModel.DataAnnotations;

namespace WebAppointments.BusinessLogic.Entity
{
    public partial class Visits
    {
        public int Id { get; set; }
        public DateTime VisitDate { get; set; }
        [Required]
        public int ParticipantId { get; set; }
        [Required]
        public int VisitSettingId { get; set; }
        [Required]
        public DateTime? NextAppointment { get; set; }
        [Required]
        public int ParticipantCame { get; set; }
        [Required]
        public int VisitOutcome { get; set; }
        public string OutcomeComment { get; set; }

        //[RequiredIf("ParticipantCame == 1", true,  ErrorMessage = "Date Participant came is required")]
        public DateTime? DateParticipantCame { get; set; }
        public DateTime? CreateDate { get; set; }
        public int Deleted { get; set; }

        public virtual Participants Participant { get; set; }
        public virtual VisitSettings VisitSetting { get; set; }
    }
}
