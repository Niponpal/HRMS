using HRMS.Core.Entities.BaseEntities;

namespace HRMS.Core.Entities;

public class EmployeeFile:AuditableEntity
{
    public long EmployeeId { get; set; }
    public Employee Employee { get; set; }
    public string FileType { get; set; } = string.Empty; // Image, Signature, NID, Document
    public string FileName { get; set; } = string.Empty;
    public string FilePath { get; set; } = string.Empty;
    public string Remarks { get; set; } = string.Empty;

}
