using HRMS.Core.Entities.BaseEntities;

namespace HRMS.Core.Entities;

public class LeaveApplication:AuditableEntity
{
    //public int LeaveId { get; set; }
    public long EmployeeId { get; set; }
    public Employee Employee { get; set; }
    public long LeaveTypeId { get; set; }
    public LeaveType LeaveType { get; set; }
    public DateTimeOffset StartDate { get; set; }
    public DateTimeOffset EndDate { get; set; }
    public decimal TotalDays { get; set; }
    public string Reason { get; set; } = string.Empty;
    public string ImagePath { get; set; } = string.Empty;
    // Workflow
    public string CurrentStage { get; set; } = "Manager"; // Manager / HR / Completed
    public string FinalStatus { get; set; } = "Pending";   // Pending / Approved / Rejected
    public ICollection<LeaveApproval> LeaveApprovals { get; set; } = new List<LeaveApproval>();

}
