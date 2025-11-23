using DinkToPdf;
using DinkToPdf.Contracts;
using HRMS.Application.FileServices;
using HRMS.Application.Logging;
using HRMS.Application.Mapping;
using HRMS.Application.Repositories;
using HRMS.Application.Repositories.Auth;
using HRMS.Application.Services;
using HRMS.Application.Services.Pdf;
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
        services.AddSingleton(typeof(IConverter), new SynchronizedConverter(new PdfTools()));
        // Register your PDF service
        services.AddScoped<IPdfService, PdfService>();
        // Register Razor view renderer
        services.AddScoped<IRazorViewToStringRenderer, RazorViewToStringRenderer>();
        services.AddAutoMapper(x => {
            x.AddMaps(typeof(IApplication).Assembly);

        });
    }
}
