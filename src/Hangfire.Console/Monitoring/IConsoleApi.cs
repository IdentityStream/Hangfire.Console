using System;
using System.Collections.Generic;
using Hangfire.Annotations;
using Hangfire.Storage.Monitoring;

namespace Hangfire.Console.Monitoring;

/// <summary>
///     Console monitoring API interface
/// </summary>
[PublicAPI]
public interface IConsoleApi : IDisposable
{
    /// <summary>
    ///     Returns lines for the console session
    /// </summary>
    /// <param name="jobId">Job identifier</param>
    /// <param name="timestamp">Time the processing was started (like, <seealso cref="StateHistoryDto.CreatedAt" />)</param>
    /// <param name="type">Type of lines to return</param>
    /// <returns>List of console lines</returns>
    IList<LineDto> GetLines(string jobId, DateTime timestamp, LineType type = LineType.Any);
}
