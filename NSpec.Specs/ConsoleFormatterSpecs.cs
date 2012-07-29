namespace NSpec.Specs
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Globalization;
    using System.IO;

    using FluentAssertions;

    using NSubstitute;

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
            Console.ForegroundColor.Should().Be(ConsoleColor.Green);
        }

        [Test]
        public void ShouldWriteError()
        {
            this.consoleFormatter.WriteError();
            this.stringWriter.Flush();
            this.stringWriter.ToString().Should().Be("F");
            Console.ForegroundColor.Should().Be(ConsoleColor.Red);
        }

        [Test]
        public void ShouldWriteSummary()
        {
            var visitor = Substitute.For<ISpecificationVisitor>();
            visitor.NumberOfExamples.Returns(10);
            visitor.NumberOfFailures.Returns(2);

            this.consoleFormatter.WriteSummary(visitor);
            this.stringWriter.Flush();
            this.stringWriter.ToString().Should().Be("10 examples, 2 failures");
            Console.ForegroundColor.Should().Be(ConsoleColor.White);
        }
    }
}