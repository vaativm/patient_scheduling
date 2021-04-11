using System;
using System.ComponentModel.DataAnnotations;

namespace WebAppointments.Models
{
    public class PSFScheduleViewModel
    {
        [Display(Name = "Study Id")]
        public string StudyId { get; set; }
        public int VisitSettingId { get; set; }
        public string VisitStage { get; set; }
        [Display(Name = "Expected Date")]
        public DateTime ExpectedDate { get; set; }
        public string AssessmentCompletion { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public string CompletionDate { get; set; }
        public string Comment { get; set; }
    }
}
