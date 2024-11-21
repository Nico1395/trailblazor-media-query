using Trailblazor.MediaQuery.Configuration.Validation;

namespace Trailblazor.MediaQuery.Configuration;

internal sealed class MediaQueryConfigurationProvider(
    IMediaQueryConfigurationValidator _mediaQueryConfigurationValidator,
    MediaQueryConfiguration _mediaQueryConfiguration) : IMediaQueryConfigurationProvider
{
    private bool _validated;

    public MediaQueryConfiguration GetConfiguration()
    {
        if (_validated)
            return _mediaQueryConfiguration;

        _mediaQueryConfigurationValidator.ValidateAndThrowIfInvalid(_mediaQueryConfiguration);
        _validated = true;

        return _mediaQueryConfiguration;
    }
}
