using HRMS.Application.CommonModel;
using HRMS.Application.Filterl;
using HRMS.Application.ViewModel;

namespace HRMS.Application.Repositories;

public interface IDepartmentReposiory
{
    Task<PaginationModel<DepartmentVm>> GetDepartmentAsync(Filter filter, CancellationToken ct);
    Task<DepartmentVm> GetDepartmentByIdAsync(long id, CancellationToken ct);
    Task<DepartmentVm> CreateOrUpdateDepartmentAsync(DepartmentVm categoryVm, CancellationToken ct);
    Task<bool> DeleteDepartmentAsync(long id, CancellationToken ct);
}
