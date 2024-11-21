namespace Trailblazor.MediaQuery.Configuration;

/// <summary>
/// Service provides the configured <see cref="MediaQueryConfiguration"/> for the media query.
/// </summary>
public interface IMediaQueryConfigurationProvider
{
    /// <summary>
    /// Service provides the configured <see cref="MediaQueryConfiguration"/>.
    /// </summary>
    /// <returns>Configured <see cref="MediaQueryConfiguration"/></returns>
    public MediaQueryConfiguration GetConfiguration();
}
