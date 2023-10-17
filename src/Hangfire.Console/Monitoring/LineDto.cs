using System;
using Hangfire.Console.Serialization;
using JetBrains.Annotations;

namespace Hangfire.Console.Monitoring;

/// <summary>
///     Base class for console lines
/// </summary>
[PublicAPI]
public abstract class LineDto
{
    internal LineDto(ConsoleLine line, DateTime referenceTimestamp)
    {
        Timestamp = referenceTimestamp.AddSeconds(line.TimeOffset);
        Color = line.TextColor;
    }

    /// <summary>
    ///     Returns type of this line
    /// </summary>
    public abstract LineType Type { get; }

    /// <summary>
    ///     Returns timestamp for the console line
    /// </summary>
    public DateTime Timestamp { get; }

    /// <summary>
    ///     Returns HTML color for the console line
    /// </summary>
    public string? Color { get; internal set; }
}
