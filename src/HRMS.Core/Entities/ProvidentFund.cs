using HRMS.Core.Entities.BaseEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Core.Entities
{
    public class ProvidentFund:AuditableEntity
    {
       
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }
        public decimal PFEmployeePercentage { get; set; } = 10m;
        public decimal PFEmployerPercentage { get; set; } = 10m;
        public DateTime EffectiveFrom { get; set; }
        public string Remarks { get; set; }

    }
}
