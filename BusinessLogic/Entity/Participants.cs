using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebAppointments.BusinessLogic.Entity
{
    public partial class Participants
    {
        public Participants()
        {
            Visits = new HashSet<Visits>();
        }

        public int Id { get; set; }
        public string StudyId { get; set; }
        public int FacilityId { get; set; }
        public int Gender { get; set; }
        public DateTime? EnrollmentDate { get; set; } 
        public string MobilePhone { get; set; }
        public string AlternateMobilePhone { get; set; }
        public string AlternateContactName { get; set; }
        public int? Deleted { get; set; }
        public virtual Facilities Facility { get; set; }
        public virtual ICollection<Visits> Visits { get; set; }

        [Display(Name = "Is PSF")]
        public bool IsPSF { get; set; }
    }
}
