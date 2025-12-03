using HRMS.Application.Expressions;
using HRMS.Application.Filterl;
using HRMS.Core.Entities;

namespace HRMS.Application.ModelSpecification;

public class EmployeeSpecification: BaseSpecification<Employee>
{
    public EmployeeSpecification(Filter filter)
    {
        // Apply filtering based on the filter criteria
        if (!string.IsNullOrWhiteSpace(filter.Search))
            ApplyCriteria(p => p.EmployeeName.Contains(filter.Search));
        // Exclude deleted records
    }

}
