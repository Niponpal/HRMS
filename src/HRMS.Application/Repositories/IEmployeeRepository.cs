using AutoMapper;
using AutoMapper.QueryableExtensions;
using HRMS.Application.CommonModel;
using HRMS.Application.Expressions;
using HRMS.Application.Extensions;
using HRMS.Application.Filterl;
using HRMS.Application.ModelSpecification;
using HRMS.Application.Services;
using HRMS.Application.ViewModel;
using HRMS.Core.Entities;
using HRMS.Infrastructure.DatabaseContext;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Application.Repositories;

public interface IEmployeeRepository
{
    Task<PaginationModel<EmployeeVm>> GetEmployeeAsync(Filter filter, CancellationToken ct);
    Task<EmployeeVm> GetEmployeeByIdAsync(long id, CancellationToken ct);
    Task<EmployeeVm> CreateOrUpdateEmployeeAsync(EmployeeVm employeeVm, CancellationToken ct);
    Task<bool> DeleteEmployeeAsync(long id, CancellationToken ct);
    Task<EmployeeVm> EmployeeUploadExcelAsync(IFormFile file, CancellationToken ct);
}


public class EmployeeRepository : IEmployeeRepository
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;
  //  private readonly IExcelUploadService _excelUploadService;
    public EmployeeRepository(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
       // _excelUploadService = excelUploadService;
    }
    public async Task<EmployeeVm> CreateOrUpdateEmployeeAsync(EmployeeVm employeeVm, CancellationToken ct)
    {
        var employee = employeeVm.Id > 0
            ? await _context.Set<Employee>().FirstOrDefaultAsync(d => d.Id == employeeVm.Id && !d.IsDelete, ct)
            : new Employee();
        if(employeeVm.Id>0 && employee == null)
            return null;
        _mapper.Map(employeeVm, employee);
        if(employeeVm.Id >0)
            _context.Update(employee);
        else 
             await _context.AddAsync(employee,ct);
             await _context.SaveChangesAsync(ct);
             return _mapper.Map<EmployeeVm>(employee);
    }

    public async Task<bool> DeleteEmployeeAsync(long id, CancellationToken ct)
    {
       var employee = await _context.Set<Employee>().FirstOrDefaultAsync(d =>d.Id == id,ct);
        if (employee == null) return false;
        employee.IsDelete = true;
        await _context.SaveChangesAsync(ct);
        return true;
    }

    public Task<EmployeeVm> EmployeeUploadExcelAsync(IFormFile file, CancellationToken ct)
    {
        throw new NotImplementedException();
    }

    public async Task<PaginationModel<EmployeeVm>> GetEmployeeAsync(Filter filter, CancellationToken ct)
    {
        var query = _context.Set<Employee>()
              .AsNoTracking()
              .Where(d => !d.IsDelete);

        query = SpecificationEvaluator<Employee>.GetQuery(query, new EmployeeSpecification(filter));

            return await query
                .ProjectTo<EmployeeVm>(_mapper.ConfigurationProvider)
                .ToPagedListAsync(filter.Page, filter.PageSize, x => x.Id, true);
    }

    public async Task<EmployeeVm> GetEmployeeByIdAsync(long id, CancellationToken ct)
    {
        var employee = await _context.Set<Employee>()
            .AsNoTracking()
            .FirstOrDefaultAsync(d => d.Id == id && !d.IsDelete, ct);
        return _mapper.Map<EmployeeVm>(employee);
    }
}