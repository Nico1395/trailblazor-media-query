using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.JSInterop;
using Trailblazor.MediaQuery.Configuration;
using Trailblazor.MediaQuery.DependencyInjection;

namespace Trailblazor.MediaQuery;

/// <summary>
/// Service component that acts as a media query. Uses configured <see cref="MediaQueryBreakpoint"/>s
/// contained by the <see cref="MediaQueryConfiguration"/>. In Order for this to work the media query has to be registered
/// using <see cref="MediaQueryDependencyInjection.AddMediaQuery"/>.
/// </summary>
public class MediaQuery : ComponentBase, IAsyncDisposable
{
    private IJSObjectReference? _mediaQueryJs;
    private DotNetObjectReference<MediaQuery>? _mediaQueryNet;
    private IMediaQueryContext? _context;

    [Inject]
    private IJSRuntime JsRuntime { get; set; } = null!;

    [Inject]
    private IMediaQueryConfigurationProvider MediaQueryConfigurationProvider { get; set; } = null!;

    [Inject]
    private IMediaQueryContextProvider MediaQueryContextProvider { get; set; } = null!;

    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    // [Parameter]
    // public bool HookIntoGlobalConfigurations { get; set; } = false;
    //
    // [Parameter]
    // public string? Query { get; set; }
    //
    // [Parameter]
    // public string? CascadingValueName { get; set; }
    
    public async ValueTask DisposeAsync()
    {
        if (_mediaQueryJs != null)
        {
            await _mediaQueryJs.InvokeVoidAsync("disposeListeners");
            await _mediaQueryJs.DisposeAsync();
        }

        _mediaQueryNet?.Dispose();
    }

    [JSInvokable(nameof(Update))]
    public void Update(string key)
    {
        var breakpoint = MediaQueryConfigurationProvider.GetConfiguration().GetBreakpoints().Single(b => b.Key == key);

        MediaQueryContextProvider.UpdateMediaQueryContext(breakpoint);
        _context = MediaQueryContextProvider.GetMediaQueryContext();

        StateHasChanged();
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (!firstRender)
            return;

        try
        {
            _mediaQueryJs = await JsRuntime.InvokeAsync<IJSObjectReference>(
                "createMediaQuery",
                MediaQueryConfigurationProvider.GetConfiguration().GetBreakpoints(),
                _mediaQueryNet ??= DotNetObjectReference.Create(this));
        }
        catch (Exception ex)
        {
            // TODO -> Allow to configure logging?
        }
    }

    protected override void BuildRenderTree(RenderTreeBuilder builder)
    {
        builder.OpenComponent<CascadingValue<IMediaQueryContext>>(0);
        builder.AddComponentParameter(1, nameof(CascadingValue<IMediaQueryContext>.Value), _context ??= MediaQueryContext.Empty);
        builder.AddComponentParameter(2, nameof(CascadingValue<IMediaQueryContext>.ChildContent), ChildContent);

        // if (CascadingValueName != null)
        //     builder.AddComponentParameter(3, nameof(CascadingValue<IMediaQueryContext>.Name), CascadingValueName);

        builder.CloseComponent();
    }
}
