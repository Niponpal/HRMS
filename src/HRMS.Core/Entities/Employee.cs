using HRMS.Core.Entities.BaseEntities;

namespace HRMS.Core.Entities;

public class Employee:AuditableEntity
{
    public string EmployeeCode { get; set; } = string.Empty;
    public string FullName { get; set; } = string.Empty;
    public string Gender { get; set; } = string.Empty;
    public DateTime? DateOfBirth { get; set; }
    public DateTime? JoinDate { get; set; }
    // FK
    public long DepartmentId { get; set; }
    public Department Department { get; set; }
    public long DesignationId { get; set; }
    public Designation Designation { get; set; }

    // Self reference for reporting manager
    public long ReportingManagerId { get; set; }
    public Employee ReportingManager { get; set; }
    public ICollection<Employee> DirectReports { get; set; } = new List<Employee>();
    public string NationalId { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string PermanentAddress { get; set; } = string.Empty;
    public string MaritalStatus { get; set; } = string.Empty;
    public string BloodGroup { get; set; } = string.Empty;
    public string EmergencyContactName { get; set; } = string.Empty;
    public string EmergencyContactPhone { get; set; } = string.Empty;
    public bool IsActive { get; set; } 
    // Navigation
    public ICollection<EmployeeFile> EmployeeFiles { get; set; } = new List<EmployeeFile>();
    public ICollection<Attendance> Attendances { get; set; } = new List<Attendance>();
    public ICollection<LeaveApplication> LeaveApplications { get; set; } = new List<LeaveApplication>();
    public ICollection<LeaveApproval> LeaveApprovals { get; set; } = new List<LeaveApproval>();
    public ICollection<Payroll> Payrolls { get; set; } = new List<Payroll>();
    public SalaryStructure CurrentSalaryStructure { get; set; }
    public ProvidentFund ProvidentFund { get; set; }

}
