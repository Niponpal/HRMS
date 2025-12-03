using HRMS.Core.Entities.BaseEntities;

namespace HRMS.Core.Entities;

public class Employee:AuditableEntity
{
    //public string EmployeeCode { get; set; } = string.Empty;
    //public string FullName { get; set; } = string.Empty;
    //public string Gender { get; set; } = string.Empty;
    //public DateTime? DateOfBirth { get; set; }
    //public DateTime? JoinDate { get; set; }

    //public long DepartmentId { get; set; }
    //public Department Department { get; set; }
    //public long DesignationId { get; set; }
    //public Designation Designation { get; set; }
    ////
    //public long SalaryStructureId { get; set; }
    //public SalaryStructure SalaryStructure { get; set; }

    //// Self reference for reporting manager
    //public long ReportingManagerId { get; set; }
    //public Employee ReportingManager { get; set; }
    //public string NationalId { get; set; } = string.Empty;
    //public string Email { get; set; } = string.Empty;
    //public string Phone { get; set; } = string.Empty;
    //public string Address { get; set; } = string.Empty;
    //public string PermanentAddress { get; set; } = string.Empty;
    //public string MaritalStatus { get; set; } = string.Empty;
    //public string BloodGroup { get; set; } = string.Empty;
    //public string EmergencyContactName { get; set; } = string.Empty;
    //public string EmergencyContactPhone { get; set; } = string.Empty;
    //public bool IsActive { get; set; }




    //Official Information

    public string EmployeeIdCardNo { get; set; }
    public string OldEmployeeIdCardNo { get; set; }
    public string RFID { get; set; }
    public string EmployeeName { get; set; }
    public string Designation { get; set; }
    public DateTime JoiningDate { get; set; }
    public string RegNo { get; set; }
    public string OfficialNumber { get; set; }
    public string OfficialEmail { get; set; }
    public string Department { get; set; }
    public string EmployeeType { get; set; }
    public DateTime? ResignationDate { get; set; }
    public bool IsActive { get; set; }
    public string CurrentWorkstation { get; set; }
    public string PalceOfPosting { get; set; }
    public string PresentDistrict { get; set; }
    public string ParmanentDistrict { get; set; }
    public string SuspensionStatusName { get; set; }
    public int? ShiftType { get; set; }


    // Personal Information

    public DateTime? DateOfBirth { get; set; }
    public string Sex { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
    public string ProfilePicture { get; set; }
    public string NID { get; set; }
    public string Religion { get; set; }
    public string Nationality { get; set; }
    public string PassportNo { get; set; }
    public string DrivingLicense { get; set; }
    public string BloodGroup { get; set; }
    public string FatherName { get; set; }
    public string MotherName { get; set; }
    public string MaritalStatus { get; set; }
    public string SpouseName { get; set; }
    public bool? IsFreedomFither { get; set; }
    public string Remarks { get; set; }
    public string TypeofDeath { get; set; }


    // Salary & Allowances Information
    public double? GrossSalary { get; set; }
    public double? BasicSalary { get; set; }
    public double? HouseRent { get; set; }
    public double? TransportAllowance { get; set; }
    public double? MedicalAllowance { get; set; }
    public double? FoodAllowance { get; set; }
    public double? BonusAllowance { get; set; }
    public double? OthersAllowance { get; set; }

    public decimal? AttendanceBonus { get; set; }
    public decimal? BreakFastAllowance { get; set; }
    public decimal? TiffinAllowance { get; set; }
    public decimal? LeaveFareAssistance { get; set; }
    public decimal? AdvanceSalary { get; set; }
    public decimal? LoanfromCompany { get; set; }
    public decimal? Fine { get; set; }
    public decimal? MobileBill { get; set; }
    public decimal? Overtime { get; set; }
    public decimal? Increment { get; set; }
    public decimal? FestivalBonus { get; set; }
    public decimal? PerformaceBonus { get; set; }

    public decimal? MaternityLeave { get; set; }
    public decimal? MaternityLeaveAmount { get; set; }










    // Navigation
    public ICollection<EmployeeFile> EmployeeFiles { get; set; } = new List<EmployeeFile>();
    public ICollection<Attendance> Attendances { get; set; } = new List<Attendance>();
    public ICollection<LeaveApplication> LeaveApplications { get; set; } = new List<LeaveApplication>();
    public ICollection<LeaveApproval> LeaveApprovals { get; set; } = new List<LeaveApproval>();
    public ICollection<HRGroupMember> HRGroupMembers { get; set; } = new List<HRGroupMember>();
    public ICollection<Payroll> Payrolls { get; set; } = new List<Payroll>();
    public ICollection<ProvidentFund> ProvidentFund { get; set; } = new List<ProvidentFund>();
}
