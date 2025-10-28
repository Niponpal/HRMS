using HRMS.Core.Entities.BaseEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Core.Entities
{
    public class Designation:AuditableEntity
    {
       
        public string DesignationName { get; set; }
        public ICollection<Employee> Employees { get; set; } = new List<Employee>();

    }
}
