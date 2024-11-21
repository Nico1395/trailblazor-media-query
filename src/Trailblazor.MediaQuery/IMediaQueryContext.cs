using Trailblazor.MediaQuery.Configuration;

namespace Trailblazor.MediaQuery;

/// <summary>
/// Context of the media query. Contains device and breakpoint, so screen dimension, information.
/// </summary>
public interface IMediaQueryContext
{
    /// <summary>
    /// Currently active breakpoint of the application.
    /// </summary>
    public IMediaQueryBreakpoint? Breakpoint { get; init; }
}