namespace Trailblazor.MediaQuery;

public interface IMediaQueryContext
{
    public IReadOnlyList<IMediaQueryBreakpoint> MatchingBreakpoints { get; }
    public bool IsActiveBreakpoint(string breakpointKey);
    internal void UpdateMatchingBreakpoints(IEnumerable<IMediaQueryBreakpoint> breakpoints);
}