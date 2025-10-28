using HRMS.Core.Entities.BaseEntities;

namespace HRMS.Core.Entities;

public class Employee:AuditableEntity
{
    
    public string EmployeeCode { get; set; }
    public string FullName { get; set; }
   
    public string Gender { get; set; }
    public DateTime? DateOfBirth { get; set; }
    public DateTime? JoinDate { get; set; }

    // FK
    public int? DepartmentId { get; set; }
    public Department Department { get; set; }

    public int? DesignationId { get; set; }
    public Designation Designation { get; set; }

    // Self reference for reporting manager
    public int? ReportingManagerId { get; set; }
    public Employee ReportingManager { get; set; }
    public ICollection<Employee> DirectReports { get; set; } = new List<Employee>();

    public string NationalId { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string Address { get; set; }
    public string PermanentAddress { get; set; }
    public string MaritalStatus { get; set; }
    public string BloodGroup { get; set; }
    public string EmergencyContactName { get; set; }
    public string EmergencyContactPhone { get; set; }
    public bool Status { get; set; } = true;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }

    // Navigation
    public ICollection<EmployeeFile> EmployeeFiles { get; set; } = new List<EmployeeFile>();
    public ICollection<Attendance> Attendances { get; set; } = new List<Attendance>();
    public ICollection<LeaveApplication> LeaveApplications { get; set; } = new List<LeaveApplication>();
    public ICollection<LeaveApproval> LeaveApprovals { get; set; } = new List<LeaveApproval>();
    public ICollection<Payroll> Payrolls { get; set; } = new List<Payroll>();
    public SalaryStructure CurrentSalaryStructure { get; set; }
    public ProvidentFund ProvidentFund { get; set; }

}
