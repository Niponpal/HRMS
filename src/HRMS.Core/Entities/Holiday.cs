using HRMS.Core.Entities.BaseEntities;

namespace HRMS.Core.Entities
{
    public class Holiday: AuditableEntity
    {

        public string Name { get; set; } = string.Empty;
        public DateTimeOffset StartDate { get; set; }
        public DateTimeOffset EndDate { get; set; }
        public string Type { get; set; } = "General"; // Govt, Religious, Company
        public string Remarks { get; set; } = string.Empty;
        public ICollection<HolidayAssignment> HolidayAssignments { get; set; } = new List<HolidayAssignment>();

    }
}
