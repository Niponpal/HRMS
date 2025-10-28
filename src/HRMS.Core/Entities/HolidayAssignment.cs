using HRMS.Core.Entities.BaseEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Core.Entities
{
    public class HolidayAssignment:AuditableEntity
    {
        public int HolidayId { get; set; }
        public Holiday Holiday { get; set; }
        public int? DepartmentId { get; set; }
        public Department Department { get; set; }
        public string ShiftName { get; set; }

    }
}
