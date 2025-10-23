using HRMS.Core.Entities.BaseEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Core.Entities
{
    public class LeaveType:AuditableEntity
    {
        public string LeaveName { get; set; }
        public int MaxDaysPerYear { get; set; }
        public bool IsPaid { get; set; } = true;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    }
}
