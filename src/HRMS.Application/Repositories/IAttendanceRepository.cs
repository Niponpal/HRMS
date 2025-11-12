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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Application.Repositories;

public interface IAttendanceRepository
{
    Task<PaginationModel<AttendanceVm>> GetAttendanceVmAsync(Filter filter, CancellationToken ct);
    Task<AttendanceVm> GetAttendanceVmByIdAsync(long id, CancellationToken ct);
    Task<AttendanceVm> CreateOrUpdateAttendanceVmAsync(AttendanceVm  attendanceVm, CancellationToken ct);
    Task<bool> DeleteAttendanceVmAsync(long id, CancellationToken ct);
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

    public Task<AttendanceVm> CreateOrUpdateAttendanceVmAsync(AttendanceVm attendanceVm, CancellationToken ct)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteAttendanceVmAsync(long id, CancellationToken ct)
    {
        throw new NotImplementedException();
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

    public Task<AttendanceVm> GetAttendanceVmByIdAsync(long id, CancellationToken ct)
    {
        throw new NotImplementedException();
    }
}