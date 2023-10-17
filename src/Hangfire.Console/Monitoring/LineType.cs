using JetBrains.Annotations;

namespace Hangfire.Console.Monitoring
{
    /// <summary>
    /// Console line type
    /// </summary>
    [PublicAPI]
    public enum LineType
    {
        /// <summary>
        /// Any type (only for filtering)
        /// </summary>
        Any,
        /// <summary>
        /// Textual line
        /// </summary>
        Text,
        /// <summary>
        /// Progress bar
        /// </summary>
        ProgressBar
    }
}
