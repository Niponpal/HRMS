using HRMS.Core.Entities.BaseEntities;

namespace HRMS.Core.Entities;

public class AdvanceSalary: AuditableEntity
{
   
    public Guid EmployeeId { get; set; }
    public decimal AdvancedSalaryAmount { get; set; }
    public decimal MonthlyDeductibleAmount { get; set; }
    public int EffectiveSalaryCycle { get; set; }
    public int EndSalaryCycle { get; set; }
}
