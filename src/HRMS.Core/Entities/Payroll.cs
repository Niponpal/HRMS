using HRMS.Core.Entities.BaseEntities;

namespace HRMS.Core.Entities;

public class Payroll:AuditableEntity
{
  
    public long EmployeeId { get; set; }
    public Employee Employee { get; set; }

    public long SalaryStructureId { get; set; }
    public SalaryStructure SalaryStructure { get; set; }

    public int PayrollMonth { get; set; }
    public int PayrollYear { get; set; }

    public decimal BasicSalary { get; set; }
    public decimal HouseRent { get; set; }
    public decimal MedicalAllowance { get; set; }
    public decimal FoodAllowance { get; set; }
    public decimal OtherAllowance { get; set; }
    public decimal GrossSalary { get; set; }

    public decimal PFEmployee { get; set; }
    public decimal PFEmployer { get; set; }
    public decimal TaxAmount { get; set; }
    public decimal NetPayable { get; set; }

    public DateTime ProcessedDate { get; set; } = DateTime.UtcNow;
    public string Status { get; set; } = "Processed";

}
