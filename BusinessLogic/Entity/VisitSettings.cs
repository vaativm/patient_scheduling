using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebAppointments.BusinessLogic.Entity
{
    public partial class VisitSettings
    {
        public VisitSettings()
        {
            Visits = new HashSet<Visits>();
        }

        public int Id { get; set; }
        [Display(Name = "Window Period")]
        public int? WindowPeriod { get; set; }
        [Display(Name = "Visit Stage")]
        public int? VisitStage { get; set; }
        [Display(Name ="Visit Type")]
        public string VisitType { get; set; }
        public string Comments { get; set; }
        public string Medication { get; set; }

        public virtual ICollection<Visits> Visits { get; set; }
    }
}
