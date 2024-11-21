namespace Trailblazor.MediaQuery.Configuration;

/// <summary>
/// Configuration of the <see cref="MediaQuery"/> service component.
/// </summary>
public interface IMediaQueryConfiguration
{
    /// <summary>
    /// Method gets the configured breakpoint <see cref="IMediaQueryBreakpoint"/>s.
    /// </summary>
    /// <returns>Configured breakpoint <see cref="IMediaQueryBreakpoint"/>s.</returns>
    public IReadOnlyList<IMediaQueryBreakpoint> GetBreakpoints();
}