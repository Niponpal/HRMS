using HRMS.Core.Entities.BaseEntities;

namespace HRMS.Core.Entities;

public class LoanFromCompany: AuditableEntity
{
    public Guid EmployeeId { get; set; }
    public decimal MonthlyDeductibleAmount { get; set; }
    public int EffectiveSalaryCycle { get; set; }
    public int EndSalaryCycle { get; set; }
}
