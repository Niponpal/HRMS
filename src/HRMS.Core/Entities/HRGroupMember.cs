using HRMS.Core.Entities.BaseEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Core.Entities
{
    public class HRGroupMember:AuditableEntity
    {
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }
        public bool IsActive { get; set; } = true;

    }
}
