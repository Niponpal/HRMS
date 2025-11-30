using HRMS.Core.Entities.BaseEntities;

namespace HRMS.Core.Entities;

public class SalaryCycle:AuditableEntity
{
    public string Name { get; set; } = string.Empty;  jan-2026
    public DateTime StartDate { get; set; }-01
    public DateTime EndDate { get; set; }-31
    public int NumberOfDays { get; set; }
}
