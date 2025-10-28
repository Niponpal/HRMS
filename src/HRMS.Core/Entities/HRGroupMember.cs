using HRMS.Core.Entities.BaseEntities;

namespace HRMS.Core.Entities;

public class HRGroupMember:AuditableEntity
{
    public long EmployeeId { get; set; }
    public Employee Employee { get; set; }
    public bool IsActive { get; set; } = true;

}
