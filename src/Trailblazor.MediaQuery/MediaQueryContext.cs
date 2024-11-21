namespace Trailblazor.MediaQuery;

internal sealed class MediaQueryContext : IMediaQueryContext
{
    private readonly List<IMediaQueryBreakpoint> _matchingBreakpoints = [];
    public IReadOnlyList<IMediaQueryBreakpoint> MatchingBreakpoints => _matchingBreakpoints;
    
    public bool IsActiveBreakpoint(string breakpointKey)
    {
        return _matchingBreakpoints.Any(b => b.Key == breakpointKey);
    }

    public void UpdateMatchingBreakpoints(IEnumerable<IMediaQueryBreakpoint> breakpoints)
    {
        _matchingBreakpoints.Clear();
        _matchingBreakpoints.AddRange(breakpoints);
    }

    internal static MediaQueryContext Empty()
    {
        return new MediaQueryContext();
    }
}