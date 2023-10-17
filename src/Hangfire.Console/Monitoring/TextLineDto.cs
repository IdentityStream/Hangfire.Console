using Hangfire.Console.Serialization;
using System;
using JetBrains.Annotations;

namespace Hangfire.Console.Monitoring
{
    /// <summary>
    /// Text console line
    /// </summary>
    [PublicAPI]
    public class TextLineDto : LineDto
    {
        internal TextLineDto(ConsoleLine line, DateTime referenceTimestamp) : base(line, referenceTimestamp)
        {
            Text = line.Message;
        }

        /// <inheritdoc />
        public override LineType Type => LineType.Text;

        /// <summary>
        /// Returns text for the console line
        /// </summary>
        public string Text { get; }
    }
}
