using Trailblazor.MediaQuery.Configuration;

namespace Trailblazor.MediaQuery;

internal sealed class MediaQueryContextProvider : IMediaQueryContextProvider
{
    private MediaQueryContext? _mediaQueryContext;

    public IMediaQueryContext? GetMediaQueryContext()
    {
        return _mediaQueryContext;
    }

    public void UpdateMediaQueryContext(IMediaQueryBreakpoint? breakpoint)
    {
        _mediaQueryContext = new MediaQueryContext()
        {
            Breakpoint = breakpoint,
        };
    }
}
