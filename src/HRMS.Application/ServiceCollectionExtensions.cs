using HRMS.Application.FileServices;
using HRMS.Application.Logging;
using HRMS.Application.Mapping;
using HRMS.Application.Repositories;
using HRMS.Application.Repositories.Auth;
using HRMS.Application.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HRMS.Application;

public static class ServiceCollectionExtensions
{
    public static void AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IDepartmentRepository, DepartmentRepository>();
        services. AddScoped<IExcelUploadService, ExcelUploadService>();
        services.AddScoped(typeof(IAppLogger<>), typeof(AppLogger<>));
        services.AddSingleton<IFileService, FileService>();
        services.AddAutoMapper(x => {
            x.AddMaps(typeof(IApplication).Assembly);

        });
    }
}
