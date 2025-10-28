using HRMS.Core.Entities.BaseEntities;

namespace HRMS.Core.Entities;

public class ProvidentFund:AuditableEntity
{
   
    public long EmployeeId { get; set; }
    public Employee Employee { get; set; }
    public decimal PFEmployeePercentage { get; set; } = 10m;
    public decimal PFEmployerPercentage { get; set; } = 10m;
    public DateTime EffectiveFrom { get; set; }
    public string Remarks { get; set; }

}
