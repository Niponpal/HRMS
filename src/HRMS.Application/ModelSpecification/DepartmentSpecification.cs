using HRMS.Application.Expressions;
using HRMS.Application.Filterl;
using HRMS.Core.Entities;

namespace HRMS.Application.ModelSpecification;

public class DepartmentSpecification :BaseSpecification<Department>
{
    /// <summary>
    /// Initializes a new instance of the DepartmentSpecification class using the specified filter criteria.
    /// </summary>
    /// <remarks>The specification applies filtering based on the provided search term and deletion status,
    /// and orders the results by department ID in descending order.</remarks>
    /// <param name="filter">The filter criteria to apply when selecting departments. The filter may include search terms and a flag
    /// indicating whether to exclude deleted records.</param>
    public DepartmentSpecification(Filter filter)
    {
        // Apply filtering based on the filter criteria
        if (!string.IsNullOrWhiteSpace(filter.Search))
            ApplyCriteria(p => p.Name.Contains(filter.Search));
        // Exclude deleted records
        if (filter.IsDelete)
            ApplyCriteria(c => !c.IsDelete);
        //  Apply ordering by Id descending
        ApplyOrderByDescending(c => c.Id);
    }
}
