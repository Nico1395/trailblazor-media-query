namespace Trailblazor.MediaQuery;

public sealed class MediaQueryConfiguration : IMediaQueryConfiguration
{
    private readonly List<IMediaQueryBreakpoint> _breakpoints = [];
    public IReadOnlyList<IMediaQueryBreakpoint> Breakpoints => _breakpoints;

    public IMediaQueryConfiguration AddBreakpoint(string key, string queryString)
    {
        var breakpoint = new MediaQueryBreakpoint()
        {
            Key = key,
            QueryString = queryString,
        };

        _breakpoints.Add(breakpoint);
        return this;
    }
}
