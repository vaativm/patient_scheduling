using System.ComponentModel.DataAnnotations;

namespace WebAppointments.Models
{
    public class ParticipantSchedule
    {
        public int VisitSettingId { get; set; }
        public string VisitType { get; set; }
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public string WindowOpens { get; set; }
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public string WindowCloses { get; set; }
        public string AssessmentCompletion { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public string CompletionDate { get; set; }
        public string Comment { get; set; }
    }
}
