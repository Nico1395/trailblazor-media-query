namespace Trailblazor.MediaQuery;

public interface IMediaQueryConfiguration
{
    public IReadOnlyList<IMediaQueryBreakpoint> Breakpoints { get; }
    public IMediaQueryConfiguration AddBreakpoint(string key, string queryString);
}