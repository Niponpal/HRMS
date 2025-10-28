using HRMS.Core.Entities.BaseEntities;

namespace HRMS.Core.Entities;

public class LeaveApprovalFlow:AuditableEntity
{
   
    public string Stage { get; set; }
    public int SequenceNo { get; set; }
    public string Description { get; set; }

}
