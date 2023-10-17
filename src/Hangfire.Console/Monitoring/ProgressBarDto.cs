using System;
using System.Globalization;
using Hangfire.Console.Serialization;
using JetBrains.Annotations;

namespace Hangfire.Console.Monitoring;

/// <summary>
///     Progress bar line
/// </summary>
[PublicAPI]
public class ProgressBarDto : LineDto
{
    internal ProgressBarDto(ConsoleLine line, DateTime referenceTimestamp) : base(line, referenceTimestamp)
    {
        Id = int.Parse(line.Message, CultureInfo.InvariantCulture);
        Name = line.ProgressName;
        Progress = line.ProgressValue!.Value;
    }

    /// <inheritdoc />
    public override LineType Type => LineType.ProgressBar;

    /// <summary>
    ///     Returns identifier for a progress bar
    /// </summary>
    public int Id { get; }

    /// <summary>
    ///     Returns optional name for a progress bar
    /// </summary>
    public string? Name { get; }

    /// <summary>
    ///     Returns progress value for a progress bar
    /// </summary>
    public double Progress { get; internal set; }
}
