namespace Trailblazor.MediaQuery.Configuration.Validation;

/// <summary>
/// Service validates a given <see cref="MediaQueryConfiguration"/>.
/// </summary>
internal interface IMediaQueryConfigurationValidator
{
    /// <summary>
    /// Method validates the given <paramref name="configuration"/> and throws a <see cref="MediaQueryConfigurationValidationException"/>
    /// if a validation error arises.
    /// </summary>
    /// <param name="configuration"><see cref="IMediaQueryConfiguration"/> to be validated.</param>
    internal void ValidateAndThrowIfInvalid(IMediaQueryConfiguration configuration);
}
