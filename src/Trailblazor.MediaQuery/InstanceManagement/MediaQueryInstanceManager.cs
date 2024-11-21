using Microsoft.JSInterop;

namespace Trailblazor.MediaQuery.InstanceManagement;

internal sealed class MediaQueryInstanceManager(IJSRuntime _jsRuntime) : IMediaQueryInstanceManager
{
    private readonly Dictionary<Guid, MediaQueryInstance> _instances = [];
    private DotNetObjectReference<MediaQueryInstanceManager>? _dotnetReference;

    private bool _isScriptLoaded = false;

    public async Task RegisterInstanceAsync(Guid instanceId, IMediaQueryConfiguration configuration, Action<IReadOnlyList<IMediaQueryBreakpoint>> callback)
    {
        await EnsureJsIsInitializedAsync();

        if (_instances.ContainsKey(instanceId))
            return;

        _instances[instanceId] = new MediaQueryInstance(configuration.Breakpoints, callback);

        await _jsRuntime.InvokeVoidAsync(
            "MediaQueryInstanceManagerJs.register",
            instanceId,
            configuration.Breakpoints,
            _dotnetReference ??= DotNetObjectReference.Create(this));
    }

    public async Task UnregisterInstanceAsync(Guid instanceId)
    {
        if (!_instances.ContainsKey(instanceId))
            return;

        _instances.Remove(instanceId);
        await _jsRuntime.InvokeVoidAsync("MediaQueryInstanceManagerJs.unregister", instanceId);
    }

    [JSInvokable("NotifyBreakpointChange")]
    public void NotifyBreakpointChange(Guid instanceId, string breakpointKey, bool isActive)
    {
        if (!_instances.TryGetValue(instanceId, out var instance))
            return;

        instance.UpdateBreakpointState(breakpointKey, isActive);
    }

    private async Task EnsureJsIsInitializedAsync()
    {
        if (_isScriptLoaded)
            return;

        await _jsRuntime.InvokeVoidAsync("import", $"./_content/Trailblazor.MediaQuery/trailblazor-media-query.js");
        _isScriptLoaded = true;
    }
}
