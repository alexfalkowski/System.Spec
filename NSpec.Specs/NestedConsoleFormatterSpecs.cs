namespace System.Spec.Specs
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Globalization;
    using System.IO;

    using FluentAssertions;

	using System.Spec.Formatter;
	using System.Spec.Specs.Properties;

    using NUnit.Framework;

    [SuppressMessage("Microsoft.Design", "CA1001:TypesThatOwnDisposableFieldsShouldBeDisposable", Justification = "It happens in AfterEach")]
    [TestFixture]
    public class NestedConsoleFormatterSpecs
    {
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
            var result = new ExampleResult { Reason = Resources.TestReason, Status = ExampleResultStatus.Success };
            this.consoleFormatter.WriteSuccess(Resources.TestReason, result);
            this.stringWriter.Flush();
            this.stringWriter.ToString().Should().Be(StringHelper.Tab + Resources.TestReason + Environment.NewLine);
        }

        [Test]
        public void ShouldWriteError()
        {
            this.consoleFormatter.WriteError(
                Resources.TestReason,
                new ExampleResult
                    {
                        Reason = Resources.TestReason,
                        Status = ExampleResultStatus.Error,
                        Exception = new InvalidOperationException("Test Exception")
                    });
            this.stringWriter.Flush();
            this.stringWriter.ToString().Should().Be(
                StringHelper.Tab + Resources.TestReason + " - System.InvalidOperationException: Test Exception" + Environment.NewLine);
        }

        [Test]
        public void ShouldWriteSummary()
        {
            this.consoleFormatter.WriteSummary(1000);
            this.stringWriter.Flush();
            this.stringWriter.ToString().Should().Be("Finished in 1 seconds" + Environment.NewLine + "0 examples, 0 failures" + Environment.NewLine);
        }
    }
}