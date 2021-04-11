using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace WebAppointments.Models
{
    public class ReportViewModel
    {
        public int ParticipantId { get; set; }
        public SelectList Participants { get; set; }
        public int VisitSettingId { get; set; }
        public IList<VisitsViewModel> Visits { get; set; } = new List<VisitsViewModel>();
    }
}
