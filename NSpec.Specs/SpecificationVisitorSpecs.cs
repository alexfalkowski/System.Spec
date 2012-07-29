namespace NSpec.Specs
{
    using System.Diagnostics.CodeAnalysis;

    using NSubstitute;

    using NUnit.Framework;

    [TestFixture]
    public class SpecificationVisitorSpecs
    {
        private const string TestReason = "test";

        private IConsoleFormatter formatter;

        private DefaultSpecificationVisitor visitor;

        [SetUp]
        public void BeforeEach()
        {
            this.formatter = Substitute.For<IConsoleFormatter>();
            this.visitor = new DefaultSpecificationVisitor(this.formatter);
        }

        [Test]
        public void ShouldVisitItWithSuccess()
        {
            var result = new ExampleResult();
            this.visitor.VisitIt(TestReason, result);
            this.formatter.Received().WriteSuccess(TestReason, result);
        }

        [Test]
        public void ShouldVisitItWithFailure()
        {
            var result = new ExampleResult { Status = ExampleResultStatus.Error };
            this.visitor.VisitIt(TestReason, result);
            this.formatter.Received().WriteError(TestReason, result);
        }

        [SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", 
            MessageId = "NSpec.IConsoleFormatter.WriteInformation(System.String)", 
            Justification = "This is a test")]
        [Test]
        public void ShouldVisitDescribe()
        {
            this.visitor.VisitDescribe(TestReason);
            this.formatter.Received().WriteInformation(TestReason);
        }
    }
}