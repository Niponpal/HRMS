using AutoMapper;
using HRMS.Application.ViewModel;
using HRMS.Core.Entities;

namespace HRMS.Application.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Department, DepartmentVm>();
    }
}

