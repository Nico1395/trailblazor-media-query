namespace Trailblazor.MediaQuery.InstanceManagement;

internal sealed class MediaQueryInstance
{
    private readonly IReadOnlyList<IMediaQueryBreakpoint> _breakpoints;
    private readonly Action<IReadOnlyList<IMediaQueryBreakpoint>> _callback;
    private readonly Dictionary<string, bool> _breakpointMatches = [];

    internal MediaQueryInstance(IReadOnlyList<IMediaQueryBreakpoint> breakpoints, Action<IReadOnlyList<IMediaQueryBreakpoint>> callback)
    {
        _breakpoints = breakpoints;
        _callback = callback;

        foreach (var breakpoint in breakpoints)
            _breakpointMatches[breakpoint.Key] = false;
    }

    public void UpdateBreakpointState(string key, bool isActive)
    {
        if (!_breakpointMatches.ContainsKey(key))
            return;

        _breakpointMatches[key] = isActive;

        var matchingBreakpoints = _breakpoints.Where(b => _breakpointMatches[b.Key] == isActive).ToList();
        _callback.Invoke(matchingBreakpoints);
    }
}