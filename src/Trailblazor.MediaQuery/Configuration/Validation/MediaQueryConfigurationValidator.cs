namespace Trailblazor.MediaQuery.Configuration.Validation;

internal sealed class MediaQueryConfigurationValidator : IMediaQueryConfigurationValidator
{
    public void ValidateAndThrowIfInvalid(IMediaQueryConfiguration configuration)
    {
        var breakpoints = configuration.GetBreakpoints();

        MakeSureKeysAreUnique(breakpoints);
        MakeSureDimensionsAreUnique(breakpoints);
    }

    private void MakeSureKeysAreUnique(IReadOnlyList<IMediaQueryBreakpoint> breakpoints)
    {
        var duplicateKeys = breakpoints
            .GroupBy(s => s.Key)
            .Where(g => g.Count() > 1)
            .Select(g => g.Key)
            .ToList();

        if (duplicateKeys.Count != 0)
            throw new MediaQueryConfigurationValidationException($"Duplicate keys found: {string.Join(", ", duplicateKeys)}");
    }

    private void MakeSureDimensionsAreUnique(IReadOnlyList<IMediaQueryBreakpoint> breakpoints)
    {
        var duplicateDimensions = breakpoints
            .GroupBy(s => s.DimensionPx)
            .Where(g => g.Count() > 1)
            .Select(g => g.Key)
            .ToList();

        if (duplicateDimensions.Count != 0)
            throw new MediaQueryConfigurationValidationException($"Duplicate dimensions found: {string.Join(", ", duplicateDimensions)}");
    }
}
