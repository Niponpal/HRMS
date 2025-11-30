using HRMS.Application.Attributes;
using HRMS.Core.Entities.BaseEntities;

namespace HRMS.Application.ViewModel;

public class DepartmentVm:BaseEntity
{
    [ExcelColumn("Department Name")]
    public string Name { get; set; } = string.Empty;
}
