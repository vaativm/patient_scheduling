using System;
using System.Collections.Generic;

namespace WebAppointments.Models
{
    public class PSFVisitsViewModel
    {
        public int VisitSettingId { get; set; }
        public int Status { get; set; }
        public DateTime? FromVisitDate { get; set; }
        public DateTime? ToVisitDate { get; set; }
        public List<PSFVisitStatusViewModel> PSFVisits { get; set; } = new List<PSFVisitStatusViewModel>();
    }
}
