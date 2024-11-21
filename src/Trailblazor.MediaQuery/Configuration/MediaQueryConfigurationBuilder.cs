namespace Trailblazor.MediaQuery.Configuration;

internal sealed class MediaQueryConfigurationBuilder : IMediaQueryConfigurationBuilder
{
    private readonly MediaQueryConfiguration _mediaQueryOptions = new();

    public IMediaQueryConfigurationBuilder AddBreakpoint(string key, uint dimensionsPx)
    {
        var existingBreakpoint = _mediaQueryOptions.Breakpoints.SingleOrDefault(b => b.Key == key);
        if (existingBreakpoint != null)
            _mediaQueryOptions.Breakpoints.Remove(existingBreakpoint);

        var breakpoint = new MediaQueryBreakpoint()
        {
            Key = key,
            DimensionPx = (int)dimensionsPx,
        };

        _mediaQueryOptions.Breakpoints.Add(breakpoint);
        return this;
    }

    public IMediaQueryConfigurationBuilder ClearBreakpoints()
    {
        _mediaQueryOptions.Breakpoints.Clear();
        return this;
    }

    public MediaQueryConfiguration Build()
    {
        return _mediaQueryOptions;
    }
}
