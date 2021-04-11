using System;
using System.Collections.Generic;
using WebAppointments.Models;

namespace WebAppointments.BusinessLogic.IService
{
    public interface IReportLogic
    {
        ReportViewModel GetReportData(DateTime? fromVisitDate, DateTime? toVisitDate, int? visitSettingId, int? participantId);
        ReportViewModel GetScheduledVisits(DateTime? fromVisitDate, DateTime? toVisitDate, int? visitSettingId);
        List<AttendanceRegisterViewModel> GetRetentionRate();
        PSFVisitsViewModel GetPSFReport(int status, DateTime? fromVisitDate, DateTime? toVisitDate);
    }
}
