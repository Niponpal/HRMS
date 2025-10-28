using HRMS.Core.Entities.BaseEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Core.Entities
{
    public class LeaveApproval:AuditableEntity
    {
       // public int ApprovalId { get; set; }
        public int LeaveId { get; set; }
        public LeaveApplication LeaveApplication { get; set; }

        public int ApproverId { get; set; }
        public Employee Approver { get; set; }

        public string Stage { get; set; } // Manager / HR
        public string ActionStatus { get; set; } = "Pending"; // Pending / Approved / Rejected
        public string Remarks { get; set; }
        public DateTime? ActionDate { get; set; }

    }
}
