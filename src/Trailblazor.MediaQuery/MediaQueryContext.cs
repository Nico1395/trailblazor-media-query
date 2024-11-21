using Trailblazor.MediaQuery.Configuration;

namespace Trailblazor.MediaQuery;

internal sealed record MediaQueryContext : IMediaQueryContext
{
    internal MediaQueryContext() { }

    public required IMediaQueryBreakpoint? Breakpoint { get; init; }

    internal static MediaQueryContext Empty => new()
    {
        Breakpoint = null,
    };
}
