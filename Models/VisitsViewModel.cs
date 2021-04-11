using System;
using System.ComponentModel.DataAnnotations;

namespace WebAppointments.Models
{
    public class VisitsViewModel
    {
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime VisitDate { get; set; }
        public string StudyId { get; set; }
        public int VisitId { get; set; }
        public string VisitType { get; set; }
        public int VisitSettingId { get; set; }
        public string Medication { get; set; }
    }
}
