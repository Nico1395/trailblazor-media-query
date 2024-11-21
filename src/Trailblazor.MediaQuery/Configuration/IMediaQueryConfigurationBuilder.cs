namespace Trailblazor.MediaQuery.Configuration;

/// <summary>
/// Builder for configuring a <see cref="MediaQueryConfiguration"/>.
/// </summary>
public interface IMediaQueryConfigurationBuilder
{
    /// <summary>
    /// Method adds a <see cref="MediaQueryBreakpoint"/> to the configuration.
    /// </summary>
    /// <remarks>
    /// If the given <paramref name="key"/> already exists, the existing breakpoint will be overridden.
    /// </remarks>
    /// <param name="key">Unique key of the breakpoint.</param>
    /// <param name="dimensionsPx">
    /// Unique dimension of the breakpoint. For more information about this dimension read the comment of
    /// <see cref="MediaQueryBreakpoint.DimensionPx"/>.
    /// </param>
    /// <returns>The <see cref="IMediaQueryConfigurationBuilder"/> for further media query configurations.</returns>
    public IMediaQueryConfigurationBuilder AddBreakpoint(string key, uint dimensionsPx);
    
    /// <summary>
    /// Method clears all <see cref="MediaQueryBreakpoint"/>s.
    /// </summary>
    /// <returns>The <see cref="IMediaQueryConfigurationBuilder"/> for further media query configurations.</returns>
    public IMediaQueryConfigurationBuilder ClearBreakpoints();
}