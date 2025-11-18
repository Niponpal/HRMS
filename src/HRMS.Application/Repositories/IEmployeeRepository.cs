using HRMS.Application.CommonModel;
using HRMS.Application.Filterl;
using HRMS.Application.ViewModel;
using Microsoft.AspNetCore.Http;

namespace HRMS.Application.Repositories;

public interface IEmployeeRepository
{
    Task<PaginationModel<EmployeeVm>> GetEmployeeAsync(Filter filter, CancellationToken ct);
    Task<EmployeeVm> GetEmployeeByIdAsync(long id, CancellationToken ct);
    Task<EmployeeVm> CreateOrUpdateEmployeeAsync(EmployeeVm employeeVm, CancellationToken ct);
    Task<EmployeeVm> DeleteEmployeeAsync(long id, CancellationToken ct);
    Task<EmployeeVm> EmployeeUploadExcelAsync(IFormFile file, CancellationToken ct);
}
