using Hangfire.Console.Progress;
using Hangfire.Console.Serialization;
using Hangfire.Console.Server;
using Hangfire.Console.Storage;
using Hangfire.Server;
using Hangfire.Storage;
using Moq;
using System;
using Xunit;

namespace Hangfire.Console.Tests
{
    public class ConsoleExtensionsFacts
    {
        private readonly Mock<IJobCancellationToken> _cancellationToken;
        private readonly Mock<JobStorageConnection> _connection;
        private readonly Mock<JobStorageTransaction> _transaction;
        private readonly Mock<JobStorage> _jobStorage;

        public ConsoleExtensionsFacts()
        {
            _cancellationToken = new Mock<IJobCancellationToken>();
            _connection = new Mock<JobStorageConnection>();
            _transaction = new Mock<JobStorageTransaction>();
            _jobStorage = new Mock<JobStorage>();

            _connection.Setup(x => x.CreateWriteTransaction())
                .Returns(_transaction.Object);
        }

        [Fact]
        public void WriteLine_DoesNotFail_IfContextIsNull()
        {
            ConsoleExtensions.WriteLine(null, "");

            _transaction.Verify(x => x.Commit(), Times.Never);
        }

        [Fact]
        public void WriteLine_Writes_IfConsoleCreated()
        {
            var context = CreatePerformContext();
            context.Items["ConsoleContext"] = CreateConsoleContext(context);

            context.WriteLine("");

            _transaction.Verify(x => x.Commit());
        }

        [Fact]
        public void WriteLine_DoesNotFail_IfConsoleNotCreated()
        {
            var context = CreatePerformContext();

            context.WriteLine("");

            _transaction.Verify(x => x.Commit(), Times.Never);
        }

        [Fact]
        public void WriteProgressBar_ReturnsNoOp_IfContextIsNull()
        {
            var progressBar = ConsoleExtensions.WriteProgressBar(null);

            Assert.IsType<NoOpProgressBar>(progressBar);
            _transaction.Verify(x => x.Commit(), Times.Never);
        }

        [Fact]
        public void WriteProgressBar_ReturnsProgressBar_IfConsoleCreated()
        {
            var context = CreatePerformContext();
            context.Items["ConsoleContext"] = CreateConsoleContext(context);

            var progressBar = context.WriteProgressBar();

            Assert.IsType<DefaultProgressBar>(progressBar);
            _transaction.Verify(x => x.Commit());
        }

        [Fact]
        public void WriteProgressBar_ReturnsNoOp_IfConsoleNotCreated()
        {
            var context = CreatePerformContext();

            var progressBar = context.WriteProgressBar();

            Assert.IsType<NoOpProgressBar>(progressBar);
            _transaction.Verify(x => x.Commit(), Times.Never);
        }

        // ReSharper disable once RedundantDisableWarningComment
#pragma warning disable xUnit1013
        // ReSharper disable once MemberCanBePrivate.Global
        public static void JobMethod() {
#pragma warning restore xUnit1013
        }

        private PerformContext CreatePerformContext()
        {
            return new PerformContext(
                _jobStorage.Object,
                _connection.Object,
                new BackgroundJob("1", Common.Job.FromExpression(() => JobMethod()), DateTime.UtcNow),
                _cancellationToken.Object);
        }

        private ConsoleContext CreateConsoleContext(PerformContext context)
        {
            return new ConsoleContext(
                new ConsoleId(context.BackgroundJob.Id, DateTime.UtcNow),
                new ConsoleStorage(context.Connection));
        }

    }
}
