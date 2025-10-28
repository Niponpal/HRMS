using HRMS.Core.Entities.BaseEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Core.Entities
{
    public class EmployeeFile:AuditableEntity
    {
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }
        public string FileType { get; set; } // Image, Signature, NID, Document
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public DateTime UploadedDate { get; set; } = DateTime.UtcNow;
        public string Remarks { get; set; }

    }
}
