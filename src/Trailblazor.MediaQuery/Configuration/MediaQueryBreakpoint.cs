namespace Trailblazor.MediaQuery.Configuration;

internal sealed record MediaQueryBreakpoint : IMediaQueryBreakpoint
{
    internal MediaQueryBreakpoint() { }

    public required string Key { get; init; }
    public required int DimensionPx { get; init; }
}
