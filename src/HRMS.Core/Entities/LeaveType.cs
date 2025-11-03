using HRMS.Core.Entities.BaseEntities;

namespace HRMS.Core.Entities;

public class LeaveType:AuditableEntity
{
    public string Name { get; set; } = string.Empty;
    public int MaxDaysPerYear { get; set; }
    public bool IsPaid { get; set; } 

   public ICollection<LeaveApplication> LeaveAllocations { get; set; } = new List<LeaveApplication>();

}
