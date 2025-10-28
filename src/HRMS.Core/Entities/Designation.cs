using HRMS.Core.Entities.BaseEntities;

namespace HRMS.Core.Entities;

public class Designation:AuditableEntity
{

    public string Name { get; set; } = string.Empty;
    public ICollection<Employee> Employees { get; set; } = new List<Employee>();

}
