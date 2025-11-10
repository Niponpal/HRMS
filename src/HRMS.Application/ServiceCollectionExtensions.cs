using HRMS.Application.Logging;
using HRMS.Application.Mapping;
using HRMS.Application.Repositories;
using HRMS.Application.Repositories.Auth;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HRMS.Application;

public static class ServiceCollectionExtensions
{
    public static void AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IDepartmentRepository, DepartmentRepository>();
        services.AddScoped(typeof(IAppLogger<>), typeof(AppLogger<>));
        services.AddAutoMapper(x => {
            x.AddMaps(typeof(IApplication).Assembly);

        });
    }
}
