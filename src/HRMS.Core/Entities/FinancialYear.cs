using HRMS.Core.Entities.BaseEntities;

namespace HRMS.Core.Entities;

public class FinancialYear:AuditableEntity
{
    public string YearName { get; set; } = string.Empty;
    public DateTime FromDate { get; set; }
    public DateTime ToDate { get; set; }
}
