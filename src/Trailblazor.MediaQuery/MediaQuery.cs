using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using Trailblazor.MediaQuery.InstanceManagement;

namespace Trailblazor.MediaQuery;

public sealed class MediaQuery : ComponentBase, IAsyncDisposable
{
    private readonly MediaQueryContext _context = MediaQueryContext.Empty();
    private readonly Guid _instanceId = Guid.NewGuid();

    [Inject]
    private IMediaQueryInstanceManager InstanceManager { get; set; } = null!;

    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    [Parameter, EditorRequired]
    public required IMediaQueryConfiguration Configuration { get; set; }

    [Parameter]
    public string? CascadingValueName { get; set; }

    public async ValueTask DisposeAsync()
    {
        await InstanceManager.UnregisterInstanceAsync(_instanceId);
    }
    
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (!firstRender)
            return;

        await InstanceManager.RegisterInstanceAsync(_instanceId, Configuration, OnBreakpointsChanged);
    }

    protected override void BuildRenderTree(RenderTreeBuilder builder)
    {
        builder.OpenComponent<CascadingValue<IMediaQueryContext>>(0);
        builder.AddComponentParameter(1, nameof(CascadingValue<IMediaQueryContext>.Value), _context);
        builder.AddComponentParameter(2, nameof(CascadingValue<IMediaQueryContext>.ChildContent), ChildContent);

        if (CascadingValueName != null)
            builder.AddComponentParameter(3, nameof(CascadingValue<IMediaQueryContext>.Name), CascadingValueName);

        builder.CloseComponent();
    }

    private void OnBreakpointsChanged(IReadOnlyList<IMediaQueryBreakpoint> matchingBreakpoints)
    {
        _context.UpdateMatchingBreakpoints(matchingBreakpoints);
        StateHasChanged();
    }
}
