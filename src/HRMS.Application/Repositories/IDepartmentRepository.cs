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
    Task<DepartmentVm> CreateOrUpdateDepartmentAsync(DepartmentVm departmentVm, CancellationToken ct);
    Task<bool> DeleteDepartmentAsync(long id, CancellationToken ct);
}

public class DepartmentRepository(ApplicationDbContext context, IMapper mapper) : IDepartmentRepository
{
    public async Task<DepartmentVm> CreateOrUpdateDepartmentAsync(DepartmentVm departmentVm, CancellationToken ct)
    {
        try
        {
            // Check if updating or creating
            var department = departmentVm.Id > 0
                ? await context.Set<Department>().FirstOrDefaultAsync(d => d.Id == departmentVm.Id, ct)
                : new Department();

            // If updating but not found, return null
            if (departmentVm.Id > 0 && department == null) return null;

            // Map ViewModel to Entity
            mapper.Map(departmentVm, department); // AutoMapper updates existing entity if provided

            // Add or Update
            if (departmentVm.Id > 0)
                context.Set<Department>().Update(department);
            else
                await context.Set<Department>().AddAsync(department, ct);

            await context.SaveChangesAsync(ct);

            // Map back to ViewModel
            return mapper.Map<DepartmentVm>(department);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            throw;
        }
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
