using HRMS.Core.Entities.BaseEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Core.Entities
{
    public class Attendance:AuditableEntity
    {
        

        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }
        public DateTime AttendanceDate { get; set; }
        public DateTime? InTime { get; set; }
        public DateTime? OutTime { get; set; }
        public string Status { get; set; } // Present, Absent, Leave, Holiday, Holiday Duty, Late
        public decimal? WorkHours { get; set; }
        public string Remarks { get; set; }

    }
}
