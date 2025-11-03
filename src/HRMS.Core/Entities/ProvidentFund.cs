using HRMS.Core.Entities.BaseEntities;

namespace HRMS.Core.Entities;

public class ProvidentFund:AuditableEntity
{
   
    public long EmployeeId { get; set; }
    public Employee Employee { get; set; }
    public decimal PFEmployeePercentage { get; set; } 
    public decimal PFEmployerPercentage { get; set; } 
    public DateTime EffectiveFrom { get; set; }
    public string Remarks { get; set; }

}
