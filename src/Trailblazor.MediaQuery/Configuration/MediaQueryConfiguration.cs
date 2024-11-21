namespace Trailblazor.MediaQuery.Configuration;

public sealed class MediaQueryConfiguration : IMediaQueryConfiguration
{
    internal MediaQueryConfiguration() { }

    internal List<MediaQueryBreakpoint> Breakpoints { get; } = [];

    public IReadOnlyList<IMediaQueryBreakpoint> GetBreakpoints()
    {
        return Breakpoints;
    }
}
