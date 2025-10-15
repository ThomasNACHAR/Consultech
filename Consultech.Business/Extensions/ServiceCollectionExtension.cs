using Consultech.Business.Abstractions;
using Consultech.Business.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Consultech.Business.Extensions;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddConsultechBusiness(this IServiceCollection services)
    {
        services.AddTransient<ISkillService, SkillService>();
        services.AddTransient<IConsultantService, ConsultantService>();
        services.AddTransient<IClientService, ClientService>();
        services.AddTransient<IMissionService, MissionService>();
        return services;
    }
}