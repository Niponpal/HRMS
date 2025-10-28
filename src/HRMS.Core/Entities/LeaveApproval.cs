using HRMS.Core.Entities.BaseEntities;

namespace HRMS.Core.Entities;

public class LeaveApproval:AuditableEntity
{
   // public int ApprovalId { get; set; }
    public long LeaveId { get; set; }
    public LeaveApplication LeaveApplication { get; set; }

    public long ApproverId { get; set; }
    public Employee Approver { get; set; }

    public string Stage { get; set; } // Manager / HR
    public string ActionStatus { get; set; } = "Pending"; // Pending / Approved / Rejected
    public string Remarks { get; set; }
    public DateTime? ActionDate { get; set; }

}
