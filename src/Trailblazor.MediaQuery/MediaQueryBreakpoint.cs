namespace Trailblazor.MediaQuery;

internal sealed class MediaQueryBreakpoint : IMediaQueryBreakpoint
{
    public required string Key { get; init; }
    public required string QueryString { get; init; }
}