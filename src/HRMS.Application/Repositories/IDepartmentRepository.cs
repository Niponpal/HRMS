using AutoMapper;
using HRMS.Application.CommonModel;
using HRMS.Application.Filterl;
using HRMS.Application.ViewModel;
using HRMS.Core.Entities;
using HRMS.Infrastructure.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Application.Repositories;

public interface IDepartmentRepository
{
    Task<PaginationModel<DepartmentVm>> GetDepartmentAsync(Filter filter, CancellationToken ct);
    Task<DepartmentVm> GetDepartmentByIdAsync(long id, CancellationToken ct);
    Task<DepartmentVm> CreateOrUpdateDepartmentAsync(DepartmentVm categoryVm, CancellationToken ct);
    Task<bool> DeleteDepartmentAsync(long id, CancellationToken ct);
}

public class DepartmentRepository(ApplicationDbContext context, IMapper mapper) : IDepartmentRepository
{
    public Task<DepartmentVm> CreateOrUpdateDepartmentAsync(DepartmentVm categoryVm, CancellationToken ct)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> DeleteDepartmentAsync(long id, CancellationToken ct)
    {
        try
        {
            var category = await context.Set<Department>()
                          .AsNoTracking()
                          .FirstOrDefaultAsync(c => c.Id == id, ct);
            if (category == null) return false;
            category.IsDelete = true;
            await context.SaveChangesAsync(ct);
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            throw;
        }
       
    }

    public Task<PaginationModel<DepartmentVm>> GetDepartmentAsync(Filter filter, CancellationToken ct)
    {
        throw new NotImplementedException();
    }

    public async Task<DepartmentVm> GetDepartmentByIdAsync(long id, CancellationToken ct)
    {
        try
        {
            var category = await context.Set<Department>()
                           .AsNoTracking()
                           .FirstOrDefaultAsync(c => c.Id == id && !c.IsDelete, ct);
            return mapper.Map<DepartmentVm>(category);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            throw;
        }
       
    }
}
