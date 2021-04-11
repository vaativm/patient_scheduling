using System.Collections.Generic;

namespace WebAppointments.Models
{
    public class RetentionRateReportViewModel
    {
        public int VisitSettingId { get; set; }
        public List<RetentionRateViewModel> RetentionRates = new List<RetentionRateViewModel>();
    }
}
