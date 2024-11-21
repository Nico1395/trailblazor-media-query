using Microsoft.Extensions.DependencyInjection;
using Trailblazor.MediaQuery.Configuration;
using Trailblazor.MediaQuery.Configuration.Validation;

namespace Trailblazor.MediaQuery.DependencyInjection;

public static class MediaQueryDependencyInjection
{
    public static IServiceCollection AddMediaQuery(this IServiceCollection services, Action<IMediaQueryConfigurationBuilder>? configuration = null)
    {
        var builder = new MediaQueryConfigurationBuilder();
        configuration?.Invoke(builder);
        var mediaQueryConfiguration = builder.Build();

        services.AddScoped<IMediaQueryConfigurationValidator, MediaQueryConfigurationValidator>();
        services.AddScoped<IMediaQueryConfigurationProvider>(sp =>
        {
            return new MediaQueryConfigurationProvider(
                sp.GetRequiredService<IMediaQueryConfigurationValidator>(),
                mediaQueryConfiguration);
        });
        services.AddScoped<IMediaQueryContextProvider, MediaQueryContextProvider>();

        return services;
    }
}
