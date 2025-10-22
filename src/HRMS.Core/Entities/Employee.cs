using HRMS.Core.Entities.BaseEntities;

namespace HRMS.Core.Entities;

public class Employee:AuditableEntity
{
    public string FullName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string? Phone { get; set; }
    public DateTime JoiningDate { get; set; }
    public decimal SalaryBase { get; set; }
    public string Status { get; set; } = "Active";
}
