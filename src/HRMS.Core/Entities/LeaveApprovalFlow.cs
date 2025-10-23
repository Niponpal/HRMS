using HRMS.Core.Entities.BaseEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Core.Entities
{
    public class LeaveApprovalFlow:AuditableEntity
    {
       
        public string Stage { get; set; }
        public int SequenceNo { get; set; }
        public string Description { get; set; }

    }
}
