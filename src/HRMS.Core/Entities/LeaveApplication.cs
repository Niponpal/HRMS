using HRMS.Core.Entities.BaseEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Core.Entities
{
    public class LeaveApplication:AuditableEntity
    {
        //public int LeaveId { get; set; }
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }

        public int LeaveTypeId { get; set; }
        public LeaveType LeaveType { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal TotalDays { get; set; }
        public string Reason { get; set; }

        // Workflow
        public string CurrentStage { get; set; } = "Manager"; // Manager / HR / Completed
        public string FinalStatus { get; set; } = "Pending";   // Pending / Approved / Rejected

        public DateTime AppliedDate { get; set; } = DateTime.UtcNow;
        public int? CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public ICollection<LeaveApproval> LeaveApprovals { get; set; } = new List<LeaveApproval>();

    }
}
