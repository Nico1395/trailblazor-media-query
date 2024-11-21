using Microsoft.Extensions.DependencyInjection;
using Trailblazor.MediaQuery.InstanceManagement;

namespace Trailblazor.MediaQuery.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddTrailblazorMediaQuery(this IServiceCollection services)
    {
        services.AddScoped<IMediaQueryInstanceManager, MediaQueryInstanceManager>();
        return services;
    }
}