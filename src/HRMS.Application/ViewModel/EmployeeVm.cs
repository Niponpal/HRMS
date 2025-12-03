using HRMS.Application.Attributes;
using HRMS.Core.Entities.BaseEntities;

namespace HRMS.Application.ViewModel;

public class EmployeeVm: BaseEntity
{


    // Employee Info
    [ExcelColumn("Employee ID Card No")]
    public string EmployeeIdCardNo { get; set; }

    [ExcelColumn("Old Employee ID Card No")]
    public string OldEmployeeIdCardNo { get; set; }

    [ExcelColumn("RFID")]
    public string RFID { get; set; }

    [ExcelColumn("Employee Name")]
    public string EmployeeName { get; set; }

    [ExcelColumn("Designation")]
    public string Designation { get; set; }

    [ExcelColumn("Joining Date")]
    public DateTime JoiningDate { get; set; }

    [ExcelColumn("Reg No")]
    public string RegNo { get; set; }

    [ExcelColumn("Official Number")]
    public string OfficialNumber { get; set; }

    [ExcelColumn("Official Email")]
    public string OfficialEmail { get; set; }

    [ExcelColumn("Department")]
    public string Department { get; set; }

    [ExcelColumn("Employee Type")]
    public string EmployeeType { get; set; }

    [ExcelColumn("Resignation Date")]
    public DateTime? ResignationDate { get; set; }

    [ExcelColumn("Is Active")]
    public bool IsActive { get; set; }

    [ExcelColumn("Current Workstation")]
    public string CurrentWorkstation { get; set; }

    [ExcelColumn("Place of Posting")]
    public string PalceOfPosting { get; set; }

    [ExcelColumn("Present District")]
    public string PresentDistrict { get; set; }

    [ExcelColumn("Permanent District")]
    public string ParmanentDistrict { get; set; }

    [ExcelColumn("Suspension Status")]
    public string SuspensionStatusName { get; set; }

    [ExcelColumn("Shift Type")]
    public int? ShiftType { get; set; }

    // Personal Information
    [ExcelColumn("Date of Birth")]
    public DateTime? DateOfBirth { get; set; }

    [ExcelColumn("Sex")]
    public string Sex { get; set; }

    [ExcelColumn("Phone Number")]
    public string PhoneNumber { get; set; }

    [ExcelColumn("Email")]
    public string Email { get; set; }

    [ExcelColumn("Profile Picture")]
    public string ProfilePicture { get; set; }

    [ExcelColumn("National ID")]
    public string NID { get; set; }

    [ExcelColumn("Religion")]
    public string Religion { get; set; }

    [ExcelColumn("Nationality")]
    public string Nationality { get; set; }

    [ExcelColumn("Passport No")]
    public string PassportNo { get; set; }

    [ExcelColumn("Driving License")]
    public string DrivingLicense { get; set; }

    [ExcelColumn("Blood Group")]
    public string BloodGroup { get; set; }

    [ExcelColumn("Father Name")]
    public string FatherName { get; set; }

    [ExcelColumn("Mother Name")]
    public string MotherName { get; set; }

    [ExcelColumn("Marital Status")]
    public string MaritalStatus { get; set; }

    [ExcelColumn("Spouse Name")]
    public string SpouseName { get; set; }

    [ExcelColumn("Is Freedom Fighter")]
    public bool? IsFreedomFither { get; set; }

    [ExcelColumn("Remarks")]
    public string Remarks { get; set; }

    [ExcelColumn("Type of Death")]
    public string TypeofDeath { get; set; }

    // Salary & Allowances
    [ExcelColumn("Gross Salary")]
    public double? GrossSalary { get; set; }

    [ExcelColumn("Basic Salary")]
    public double? BasicSalary { get; set; }

    [ExcelColumn("House Rent")]
    public double? HouseRent { get; set; }

    [ExcelColumn("Transport Allowance")]
    public double? TransportAllowance { get; set; }

    [ExcelColumn("Medical Allowance")]
    public double? MedicalAllowance { get; set; }

    [ExcelColumn("Food Allowance")]
    public double? FoodAllowance { get; set; }

    [ExcelColumn("Bonus Allowance")]
    public double? BonusAllowance { get; set; }

    [ExcelColumn("Other Allowance")]
    public double? OthersAllowance { get; set; }

    [ExcelColumn("Attendance Bonus")]
    public decimal? AttendanceBonus { get; set; }

    [ExcelColumn("Breakfast Allowance")]
    public decimal? BreakFastAllowance { get; set; }

    [ExcelColumn("Tiffin Allowance")]
    public decimal? TiffinAllowance { get; set; }

    [ExcelColumn("Leave Fare Assistance")]
    public decimal? LeaveFareAssistance { get; set; }

    [ExcelColumn("Advance Salary")]
    public decimal? AdvanceSalary { get; set; }

    [ExcelColumn("Loan from Company")]
    public decimal? LoanfromCompany { get; set; }

    [ExcelColumn("Fine")]
    public decimal? Fine { get; set; }

    [ExcelColumn("Mobile Bill")]
    public decimal? MobileBill { get; set; }

    [ExcelColumn("Overtime")]
    public decimal? Overtime { get; set; }

    [ExcelColumn("Increment")]
    public decimal? Increment { get; set; }

    [ExcelColumn("Festival Bonus")]
    public decimal? FestivalBonus { get; set; }

    [ExcelColumn("Performance Bonus")]
    public decimal? PerformaceBonus { get; set; }

    [ExcelColumn("Maternity Leave")]
    public decimal? MaternityLeave { get; set; }

    [ExcelColumn("Maternity Leave Amount")]
    public decimal? MaternityLeaveAmount { get; set; }


    //[ExcelColumn("Employee Name")]
    //public string FullName { get; set; } = string.Empty;
    //[ExcelColumn("Employee Gender")]
    //public string Gender { get; set; } = string.Empty;
    //[ExcelColumn("Employee DateOfBirth")]
    //public DateTime? DateOfBirth { get; set; }
    //[ExcelColumn("Employee JoinDate")]
    //public DateTime? JoinDate { get; set; }
    //[ExcelColumn("Employee NationalId")]
    //public string NationalId { get; set; } = string.Empty;
    //[ExcelColumn("Employee Email")]
    //public string Email { get; set; } = string.Empty;
    //[ExcelColumn("Employee Phone")]
    //public string Phone { get; set; } = string.Empty;
    //[ExcelColumn("Employee Address")]
    //public string Address { get; set; } = string.Empty;
    //[ExcelColumn("Employee Permanent Address")]
    //public string PermanentAddress { get; set; } = string.Empty;
    //[ExcelColumn("Employee Marital Status")]
    //public string MaritalStatus { get; set; } = string.Empty;
    //[ExcelColumn("Employee Blood Group")]
    //public string BloodGroup { get; set; } = string.Empty;
    //[ExcelColumn("Employee Emergency Contact Name")]
    //public string EmergencyContactName { get; set; } = string.Empty;
    //[ExcelColumn("Employee Emergency Contact Phone")]
    //public string EmergencyContactPhone { get; set; } = string.Empty;
    //[ExcelColumn("Is Active")]
    //public bool IsActive { get; set; }

    // Foreign Keys
    public long DepartmentId { get; set; }
    public long DesignationId { get; set; }
    public long SalaryStructureId { get; set; }
    public long ReportingManagerId { get; set; }

}
