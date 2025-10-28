using HRMS.Core.Entities.BaseEntities;

namespace HRMS.Core.Entities
{
    public class Holiday: AuditableEntity
    {
       
        public string HolidayName { get; set; }
        public DateTime HolidayDate { get; set; }
        public string HolidayType { get; set; } = "General"; // Govt, Religious, Company
        public bool IsRecurring { get; set; } = false;
        public string Remarks { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public ICollection<HolidayAssignment> HolidayAssignments { get; set; } = new List<HolidayAssignment>();

    }
}
