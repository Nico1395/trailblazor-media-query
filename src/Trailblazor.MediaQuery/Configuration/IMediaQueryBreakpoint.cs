namespace Trailblazor.MediaQuery.Configuration;

/// <summary>
/// Setting for a media query breakpoint.
/// </summary>
public interface IMediaQueryBreakpoint
{
    /// <summary>
    /// Unique key of the breakpoint.
    /// </summary>
    public string Key { get; }

    /// <summary>
    /// Breakpoint dimension in pixels.
    /// </summary>
    /// <remarks>
    /// <para>
    /// If this is the smallest breakpoint, this will be the upper boundary - 1.
    /// </para>
    /// <para>
    /// If this is the largest breakpoint, this will be the lower boundary.
    /// </para>
    /// <para>
    /// If this breakpoint is anywhere in between, the previous and next breakpoints dimensions
    /// will be used as lower and upper boundaries.
    /// </para>
    /// </remarks>
    public int DimensionPx { get; }
}