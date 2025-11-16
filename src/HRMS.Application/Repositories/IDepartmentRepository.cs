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

namespace HRMS.Application.Repositories
{
    public interface IDepartmentRepository
    {
        Task<PaginationModel<DepartmentVm>> GetDepartmentAsync(Filter filter, CancellationToken ct);
        Task<DepartmentVm> GetDepartmentByIdAsync(long id, CancellationToken ct);
        Task<DepartmentVm> CreateOrUpdateDepartmentAsync(DepartmentVm departmentVm, CancellationToken ct);
        Task<bool> DeleteDepartmentAsync(long id, CancellationToken ct);
        Task<string> DepartmentUploadExcelAsync(IFormFile file, CancellationToken ct);
    }

    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IExcelUploadService _excelUploadService;

        public DepartmentRepository(ApplicationDbContext context, IMapper mapper, IExcelUploadService excelUploadService)
        {
            _context = context;
            _mapper = mapper;
            _excelUploadService = excelUploadService;
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

            if (departmentVm.Id > 0 && department == null)
                return null;

            _mapper.Map(departmentVm, department);

            if (departmentVm.Id > 0)
                _context.Update(department);
            else
                await _context.AddAsync(department, ct);

            await _context.SaveChangesAsync(ct);

            return _mapper.Map<DepartmentVm>(department);
        }

        public async Task<bool> DeleteDepartmentAsync(long id, CancellationToken ct)
        {
            var department = await _context.Set<Department>().FindAsync(id, ct);
            if (department == null) return false;

            department.IsDelete = true;
            await _context.SaveChangesAsync(ct);
            return true;
        }

        // ✅ Excel Upload Implementation (fixed)
        public async Task<string> DepartmentUploadExcelAsync(IFormFile file, CancellationToken ct)
        {
            try
            {
                if (file == null || file.Length == 0)
                    return $"No file uploaded.";
                // Read and map Excel data
                using var stream = file.OpenReadStream();
                // ✅ Use the ExcelUploadService to read and map Excel data to DepartmentVm
                var departmentVms = _excelUploadService.ReadExcelToModel<DepartmentVm>(stream);
                // preload existing department names for fast lookup
                var existingNames = await _context.Set<Department>()
                    .Where(d => !d.IsDelete)
                    .Select(d => d.Name.ToLower()) // case-insensitive
                    .ToListAsync(ct);

                // filter out duplicates
                var newDepartments = departmentVms
                    .Where(d => !existingNames.Contains(d.Name.ToLower()))
                    .Select(d => _mapper.Map<Department>(d))
                    .ToList();

                // bulk insert
                if (newDepartments.Any())
                    await _context.AddRangeAsync(newDepartments, ct);

                await _context.SaveChangesAsync(ct);

                // ✅ Return only the count of inserted rows
                return $"Uploaded {newDepartments.Count} new departments successfully.";
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

    }
}
