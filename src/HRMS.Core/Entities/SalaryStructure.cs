using HRMS.Core.Entities.BaseEntities;

namespace HRMS.Core.Entities;

public class SalaryStructure:AuditableEntity
{
    public long EmployeeId { get; set; }
    public Employee Employee { get; set; }
    public DateTime EffectiveFrom { get; set; }
    public decimal BasicSalary { get; set; }
    public decimal HouseRent { get; set; }
    public decimal MedicalAllowance { get; set; }
    public decimal FoodAllowance { get; set; }
    public decimal OtherAllowance { get; set; }

    // GrossSalary is computed: configured with Fluent API
    public decimal GrossSalary { get; private set; }
    public bool IsActive { get; set; } = true;

}
