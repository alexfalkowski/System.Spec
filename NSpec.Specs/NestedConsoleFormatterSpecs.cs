namespace NSpec.Specs
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Globalization;
    using System.IO;

    using FluentAssertions;

    using NUnit.Framework;

    [SuppressMessage("Microsoft.Design", "CA1001:TypesThatOwnDisposableFieldsShouldBeDisposable", Justification = "It happens in AfterEach")]
    [TestFixture]
    public class NestedConsoleFormatterSpecs
    {
        private const string TestReason = "test";

        private StringWriter stringWriter;

        private IConsoleFormatter consoleFormatter;

        [SetUp]
        public void BeforeEach()
        {
            this.stringWriter = new StringWriter(CultureInfo.CurrentCulture);
            Console.SetOut(this.stringWriter);

            this.consoleFormatter = new NestedConsoleFormatter();
        }

        [TearDown]
        public void AfterEach()
        {
            this.stringWriter.Dispose();
        }

        [Test]
        public void ShouldWriteSuccess()
        {
            var result = new ExampleResult { Reason = TestReason, Status = ExampleResultStatus.Success };
            this.consoleFormatter.WriteSuccess(TestReason, result);
            this.stringWriter.Flush();
            this.stringWriter.ToString().Should().Be(StringHelper.Tab + TestReason + Environment.NewLine);
        }

        [Test]
        public void ShouldWriteError()
        {
            this.consoleFormatter.WriteError(
                TestReason,
                new ExampleResult
                    {
                        Reason = TestReason,
                        Status = ExampleResultStatus.Error,
                        Exception = new InvalidOperationException("Test Exception")
                    });
            this.stringWriter.Flush();
            this.stringWriter.ToString().Should().Be(
                StringHelper.Tab + TestReason + " - System.InvalidOperationException: Test Exception\r\n");
        }

        [Test]
        public void ShouldWriteSummary()
        {
            this.consoleFormatter.WriteSummary(1000);
            this.stringWriter.Flush();
            this.stringWriter.ToString().Should().Be("Finished in 1 seconds\r\n0 examples, 0 failures\r\n");
        }
    }
}