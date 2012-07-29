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
    public class ConsoleFormatterSpecs
    {
        private StringWriter stringWriter;

        private IConsoleFormatter consoleFormatter;

        [SetUp]
        public void BeforeEach()
        {
            this.stringWriter = new StringWriter(CultureInfo.CurrentCulture);
            Console.SetOut(this.stringWriter);

            this.consoleFormatter = new DefaultConsoleFormatter();
        }

        [TearDown]
        public void AfterEach()
        {
            this.stringWriter.Dispose();
        }

        [Test]
        public void ShouldWriteSuccess()
        {
            this.consoleFormatter.WriteSuccess();
            this.stringWriter.Flush();
            this.stringWriter.ToString().Should().Be(".");
        }

        [Test]
        public void ShouldWriteError()
        {
            this.consoleFormatter.WriteError();
            this.stringWriter.Flush();
            this.stringWriter.ToString().Should().Be("F");
        }

        [Test]
        public void ShouldWriteSummary()
        {
            this.consoleFormatter.WriteSummary(10, 2, 1000);
            this.stringWriter.Flush();
            this.stringWriter.ToString().Should().Be("Finished in 1 seconds\r\n10 examples, 2 failures\r\n");
        }

        [Test]
        public void ShouldWriteErrors()
        {
            var result = new ExampleResult
                {
                    Reason = "it 1",
                    Exception = new InvalidOperationException("Test Exception"), 
                    Status = ExampleResultStatus.Failure
                };
            this.consoleFormatter.WriteErrors(new[] { result });
            this.stringWriter.Flush();
            this.stringWriter.ToString().Should().Be("\r\n\r\n1) it 1 - System.InvalidOperationException: Test Exception\r\n\r\n");
        }
    }
}