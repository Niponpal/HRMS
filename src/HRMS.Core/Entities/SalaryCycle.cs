using HRMS.Core.Entities.BaseEntities;

namespace HRMS.Core.Entities;

public class SalaryCycle:AuditableEntity
{
    public string Name { get; set; } = string.Empty; 
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public int NumberOfDays { get; set; }
}
