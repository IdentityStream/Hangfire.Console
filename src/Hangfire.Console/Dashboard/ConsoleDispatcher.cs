using System;
using System.Text;
using System.Threading.Tasks;
using Hangfire.Console.Serialization;
using Hangfire.Console.Storage;
using Hangfire.Dashboard;

namespace Hangfire.Console.Dashboard;

/// <summary>
///     Provides incremental updates for a console.
/// </summary>
internal class ConsoleDispatcher : IDashboardDispatcher
{
    public Task Dispatch(DashboardContext context)
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        var consoleId = ConsoleId.Parse(context.UriMatch.Groups[1].Value);

        var startArg = context.Request.GetQuery("start");

        // try to parse offset at which we should start returning requests
        if (string.IsNullOrEmpty(startArg) || !int.TryParse(startArg, out var start))
        {
            // if not provided or invalid, fetch records from the very start
            start = 0;
        }

        var buffer = new StringBuilder();
        using (var storage = new ConsoleStorage(context.Storage.GetConnection()))
        {
            ConsoleRenderer.RenderLineBuffer(buffer, storage, consoleId, start);
        }

        context.Response.ContentType = "text/html";
        return context.Response.WriteAsync(buffer.ToString());
    }
}
