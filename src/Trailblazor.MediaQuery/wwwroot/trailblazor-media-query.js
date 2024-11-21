/**
 * Setting for a media query breakpoint.
 */
class MediaQueryBreakpointSetting {
    constructor(dimensionPx, key) {
        this.dimensionPx = dimensionPx;
        this.key = key;
    }
}

/**
 * Configuration of the media query service.
 */
class MediaQueryConfiguration {
    constructor(breakpointSettings) {
        this.breakpointSettings = Array.isArray(breakpointSettings) ? breakpointSettings : [];
        this.breakpointSettings.sort((a, b) => a.dimensionPx - b.dimensionPx);                      // Sort breakpoints by dimensionPx in ascending order
    }
}

/**
 * Media query service handling attaching queries and invoking the Razor side of the media query service.
 */
class TrailblazorMediaQuery {

    /**
     * Instantiates a media query service.
     * @param {any} mediaQueryConfiguration Configuration of the media query service.
     * @param {any} mediaQueryNet The Razor media query component. Required for notifying it upon relevant changes.
     */
    constructor(mediaQueryConfiguration, mediaQueryNet) {
        this.mediaQueryConfiguration = mediaQueryConfiguration;
        this.mediaQueryNet = mediaQueryNet;
        this.currentBreakpoint = null;
        this.mediaQueryListeners = [];

        this.disposeListeners();
        this.initMediaQueryListeners();
    }

    /**
     * Method initializes the query listeners.
     * 
     * Following logic is being applied when listening to the viewports dimensions:
     * - The smallest breakpoint uses its dimension as an upper boundary.
     * - The largest breakpoint uses its dimension as the lower boundary.
     * - Breakpoints inbetween use their neighbouring, so previous and next breakpoints, boundaries.
     */
    initMediaQueryListeners() {
        this.mediaQueryConfiguration.breakpointSettings.forEach((setting, index) => {
            const prevBreakpoint = this.mediaQueryConfiguration.breakpointSettings[index - 1];
            const nextBreakpoint = this.mediaQueryConfiguration.breakpointSettings[index + 1];

            let rangeQuery;

            if (index === 0) {
                rangeQuery = `(max-width: ${setting.dimensionPx - 1}px)`;
            }
            else if (prevBreakpoint != null && nextBreakpoint != null) {
                rangeQuery = `(min-width: ${prevBreakpoint.dimensionPx}px) and (max-width: ${nextBreakpoint.dimensionPx - 1}px)`;
            }
            else {
                rangeQuery = `(min-width: ${setting.dimensionPx}px)`;
            }

            const mediaQueryList = window.matchMedia(rangeQuery);
            const listener = (event) => {
                if (event.matches) {
                    this.notifyRazorMediaQuery(setting);
                }
            };

            mediaQueryList.addListener(listener);
            this.mediaQueryListeners.push({ mediaQueryList, listener });

            if (mediaQueryList.matches) {
                this.notifyRazorMediaQuery(setting);
            }
        });
    }

    /**
     * Method notifies the related Razor media query component about the provided breakpointSetting
     * being triggered. This breakpoint is the new breakpoint.
     * @param {MediaQueryBreakpointSetting} breakpointSetting
     */
    notifyRazorMediaQuery(breakpointSetting) {
        if (breakpointSetting == null) {
            return;
        }

        if (this.currentBreakpoint?.key !== breakpointSetting?.key) {
            this.currentBreakpoint = breakpointSetting;
            this.mediaQueryNet.invokeMethodAsync('Update', breakpointSetting.key);
        }
    }

    /**
     * Method disposes off of the media query listeners.
     */
    disposeListeners() {
        this.mediaQueryListeners.forEach(({ mediaQueryList, listener }) => {
            mediaQueryList.removeListener(listener);
        });
        this.mediaQueryListeners = [];
    }
}

/**
 * Factory function creates a media query service.
 * @param {any} breakpointSettings Breakpoint settings configured in .NET.
 * @param {any} mediaQueryNet The Razor media query component. Required for notifying it upon relevant changes.
 * @returns Media query service object, so the Razor media query has a JSObjectReference to the service for safe disposal.
 */
function createMediaQuery(breakpointSettings, mediaQueryNet) {
    const config = new MediaQueryConfiguration(breakpointSettings || []);
    return new TrailblazorMediaQuery(config, mediaQueryNet);
}
