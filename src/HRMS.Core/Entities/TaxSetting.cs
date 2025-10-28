using HRMS.Core.Entities.BaseEntities;

namespace HRMS.Core.Entities;

public class TaxSetting:AuditableEntity
{ 
    public int TaxYear { get; set; }
    public decimal MinIncome { get; set; }
    public decimal MaxIncome { get; set; }
    public decimal TaxPercentage { get; set; }

}
