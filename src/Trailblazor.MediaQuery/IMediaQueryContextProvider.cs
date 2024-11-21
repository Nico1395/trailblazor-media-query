using Trailblazor.MediaQuery.Configuration;

namespace Trailblazor.MediaQuery;

/// <summary>
/// Service provides the current <see cref="MediaQueryContext"/>.
/// </summary>
public interface IMediaQueryContextProvider
{
    /// <summary>
    /// Method gets the current <see cref="MediaQueryContext"/>.
    /// </summary>
    /// <returns>Current <see cref="MediaQueryContext"/>.</returns>
    public IMediaQueryContext? GetMediaQueryContext();

    /// <summary>
    /// Method allows updating the provider using the given information.
    /// </summary>
    /// <param name="breakpoint">New current <see cref="MediaQueryBreakpoint"/>.</param>
    public void UpdateMediaQueryContext(IMediaQueryBreakpoint? breakpoint);
}
