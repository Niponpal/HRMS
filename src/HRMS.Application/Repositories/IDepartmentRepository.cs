using AutoMapper;
using AutoMapper.QueryableExtensions;
using HRMS.Application.CommonModel;
using HRMS.Application.Expressions;
using HRMS.Application.Extensions;
using HRMS.Application.Filterl;
using HRMS.Application.ModelSpecification;
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

public class DepartmentRepository : IDepartmentRepository
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public DepartmentRepository(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PaginationModel<DepartmentVm>> GetDepartmentAsync(Filter filter, CancellationToken ct)
    {
        var query = _context.Set<Department>()
            .AsNoTracking()
            .Where(d => !d.IsDelete);

        query = SpecificationEvaluator<Department>.GetQuery(query, new DepartmentSpecification(filter));

        return await query
            .ProjectTo<DepartmentVm>(_mapper.ConfigurationProvider)
            .ToPagedListAsync(filter.Page, filter.PageSize, x => x.Id, true);
    }

    public async Task<DepartmentVm> GetDepartmentByIdAsync(long id, CancellationToken ct)
    {
        var department = await _context.Set<Department>()
            .AsNoTracking()
            .FirstOrDefaultAsync(d => d.Id == id && !d.IsDelete, ct);

        return _mapper.Map<DepartmentVm>(department);
    }

    public async Task<DepartmentVm> CreateOrUpdateDepartmentAsync(DepartmentVm departmentVm, CancellationToken ct)
    {
        var department = departmentVm.Id > 0
            ? await _context.Set<Department>().FirstOrDefaultAsync(d => d.Id == departmentVm.Id && !d.IsDelete, ct)
            : new Department();

        if (departmentVm.Id > 0 && department == null) return null;

        _mapper.Map(departmentVm, department);

        if (departmentVm.Id > 0)
            _context.Set<Department>().Update(department);
        else
            await _context.Set<Department>().AddAsync(department, ct);

        await _context.SaveChangesAsync(ct);

        return _mapper.Map<DepartmentVm>(department);
    }

    public async Task<bool> DeleteDepartmentAsync(long id, CancellationToken ct)
    {
        var department = await _context.Set<Department>().FirstOrDefaultAsync(d => d.Id == id, ct);
        if (department == null) return false;

        department.IsDelete = true;
        await _context.SaveChangesAsync(ct);

        return true;
    }
}
