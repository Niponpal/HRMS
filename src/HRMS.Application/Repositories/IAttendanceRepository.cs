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
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Application.Repositories;

public interface IAttendanceRepository
{
    Task<PaginationModel<AttendanceVm>> GetAttendanceVmAsync(Filter filter, CancellationToken ct);
    Task<AttendanceVm> GetAttendanceVmByIdAsync(long id, CancellationToken ct);
    Task<AttendanceVm> CreateOrUpdateAttendanceVmAsync(AttendanceVm  attendanceVm, CancellationToken ct);
    Task<bool> DeleteAttendanceVmAsync(long id, CancellationToken ct);
    Task<string> AttendanceUploadExcelAsync(IFormFile file, CancellationToken ct);
}


public class AttendanceRepository : IAttendanceRepository
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public AttendanceRepository(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public Task<string> AttendanceUploadExcelAsync(IFormFile file, CancellationToken ct)
    {
        throw new NotImplementedException();
    }

    public async Task<AttendanceVm> CreateOrUpdateAttendanceVmAsync(AttendanceVm attendanceVm, CancellationToken ct)
    {
       var attendance = attendanceVm.Id>0
            ? await  _context.Set<Attendance>().FirstOrDefaultAsync(d => d.Id == attendanceVm.Id && !d.IsDelete,ct)
            : new Attendance(); 
        if(attendanceVm.Id>0 && attendance == null)
            return null;
        _mapper.Map(attendanceVm, attendance);
        if(attendanceVm.Id>0)
            _context.Update(attendance);
        else 
            await _context.AddAsync(attendance, ct);
        await _context.SaveChangesAsync(ct);
        return _mapper.Map<AttendanceVm>(attendance);
    }

    public async Task<bool> DeleteAttendanceVmAsync(long id, CancellationToken ct)
    {
       var attendance =  await _context.Set<Attendance>().FirstOrDefaultAsync(d=>d.Id==id, ct);
        if (attendance == null) return false;
       
        attendance.IsDelete = true;
        await _context.SaveChangesAsync(ct);
        return true;
    }

    public async Task<PaginationModel<AttendanceVm>> GetAttendanceVmAsync(Filter filter, CancellationToken ct)
    {
        var query = _context.Set<Attendance>()
            .AsNoTracking()
            .Where(d => !d.IsDelete);
        query = SpecificationEvaluator<Attendance>.GetQuery(query, new AttendanceSpecification(filter));

        return await query
            .ProjectTo<AttendanceVm>(_mapper.ConfigurationProvider)
            .ToPagedListAsync(filter.Page, filter.PageSize, x => x.Id, true);
    }

    public async Task<AttendanceVm> GetAttendanceVmByIdAsync(long id, CancellationToken ct)
    {
        var attendance = await  _context.Set<Attendance>()
            .AsNoTracking()
            .FirstOrDefaultAsync(d=>d.Id == id && !d.IsDelete,ct);

        return _mapper.Map<AttendanceVm>(attendance);
    }
}