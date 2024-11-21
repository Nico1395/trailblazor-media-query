namespace Trailblazor.MediaQuery.Configuration.Validation;

/// <summary>
/// Exception expresses that an error arose during validating the configured <see cref="MediaQueryConfiguration"/>.
/// </summary>
/// <param name="message">Message containing error details.</param>
public sealed class MediaQueryConfigurationValidationException(string message) : Exception(message)
{
}
