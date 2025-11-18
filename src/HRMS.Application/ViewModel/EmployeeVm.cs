using HRMS.Application.Attributes;
using HRMS.Core.Entities.BaseEntities;

namespace HRMS.Application.ViewModel;

public class EmployeeVm:BaseEntity
{
    [ExcelColumn("Employee Name")]
    public string FullName { get; set; } = string.Empty;
    [ExcelColumn("Employee Gender")]
    public string Gender { get; set; } = string.Empty;
    [ExcelColumn("Employee DateOfBirth")]
    public DateTime? DateOfBirth { get; set; }
    [ExcelColumn("Employee JoinDate")]
    public DateTime? JoinDate { get; set; }
    [ExcelColumn("Employee NationalId")]
    public string NationalId { get; set; } = string.Empty;
    [ExcelColumn("Employee Email")]
    public string Email { get; set; } = string.Empty;
    [ExcelColumn("Employee Phone")]
    public string Phone { get; set; } = string.Empty;
    [ExcelColumn("Employee Address")]
    public string Address { get; set; } = string.Empty;
    [ExcelColumn("Employee Permanent Address")]
    public string PermanentAddress { get; set; } = string.Empty;
    [ExcelColumn("Employee Marital Status")]
    public string MaritalStatus { get; set; } = string.Empty;
    [ExcelColumn("Employee Blood Group")]
    public string BloodGroup { get; set; } = string.Empty;
    [ExcelColumn("Employee Emergency Contact Name")]
    public string EmergencyContactName { get; set; } = string.Empty;
    [ExcelColumn("Employee Emergency Contact Phone")]
    public string EmergencyContactPhone { get; set; } = string.Empty;
    [ExcelColumn("Is Active")]
    public bool IsActive { get; set; }

    // Foreign Keys
    public long DepartmentId { get; set; }
    public long DesignationId { get; set; }
    public long SalaryStructureId { get; set; }
    public long ReportingManagerId { get; set; }

}
