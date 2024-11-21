namespace Trailblazor.MediaQuery.InstanceManagement;

internal interface IMediaQueryInstanceManager
{
    internal Task RegisterInstanceAsync(Guid instanceId, IMediaQueryConfiguration configuration, Action<IReadOnlyList<IMediaQueryBreakpoint>> callback);
    internal Task UnregisterInstanceAsync(Guid instanceId);
}