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
    public class ProgressConsoleFormatterSpecs
    {
        private StringWriter stringWriter;
        private IConsoleFormatter consoleFormatter;
        private TextWriter originalWritter;

        [SetUp]
        public void BeforeEach()
        {
            this.originalWritter = Console.Out;
            this.stringWriter = new StringWriter(CultureInfo.CurrentCulture);
            Console.SetOut(this.stringWriter);

            this.consoleFormatter = new ProgressConsoleFormatter();
            this.consoleFormatter.WriteInformation(Resources.TestReason);
        }

        [TearDown]
        public void AfterEach()
        {
            this.stringWriter.Dispose();
            Console.SetOut(this.originalWritter);
        }

        [Test]
        public void ShouldWriteSuccess()
        {
            this.consoleFormatter.WriteSuccess(Resources.TestReason, new ExampleResult());
            this.stringWriter.Flush();
            this.stringWriter.ToString().Should().Be(".");
        }

        [Test]
        public void ShouldWriteError()
        {
            this.consoleFormatter.WriteError(Resources.TestReason, new ExampleResult());
            this.stringWriter.Flush();
            this.stringWriter.ToString().Should().Be("F");
        }

        [Test]
        public void ShouldWriteSummary()
        {
            var example = new ExampleResult
                {
                    Reason = Resources.TestReason,
                    ElapsedTime = 1000,
                    Exception = new InvalidOperationException("Test Exception"),
                    Status = ResultStatus.Error
                };
            var results = new ExpressionResultCollection();
            var expression = new ExpressionResult();
            var group = new ExampleGroupResult();
            group.Examples.Add(example);
            expression.Examples.Add(group);
            results.Add(expression);

            this.consoleFormatter.WriteError(Resources.TestReason, example);
            this.consoleFormatter.WriteSummary(results);
            this.stringWriter.Flush();
            this.stringWriter.ToString().Should().Be(
                "F" + Environment.NewLine + Environment.NewLine + "1) test - System.InvalidOperationException: Test Exception" + Environment.NewLine + Environment.NewLine + "Finished in 1 seconds" + Environment.NewLine + "1 examples, 1 failures" + Environment.NewLine);
        }
    }
}