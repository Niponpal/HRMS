using HRMS.Application.Expressions;
using HRMS.Application.Filterl;
using HRMS.Core.Entities;

namespace HRMS.Application.ModelSpecification;

public class AttendanceSpecification: BaseSpecification<Attendance>
{
    public AttendanceSpecification(Filter filter)
    {
        // Apply filtering based on the filter criteria
        if (!string.IsNullOrWhiteSpace(filter.Search))
            ApplyCriteria(p => p.Status.Contains(filter.Search));
        // Exclude deleted records
        if (filter.IsDelete)
            ApplyCriteria(c => !c.IsDelete);
        //  Apply ordering by Id descending
        ApplyOrderByDescending(c => c.Id);
    }
}
