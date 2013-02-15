namespace System.Spec.Specs
{
	using System.Spec.Formatter;
	using System.Spec.Specs.Properties;

	using NSubstitute;

	using NUnit.Framework;

	[TestFixture]
	public class SpecificationVisitorSpecs
	{
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
			this.visitor.VisitIt(Resources.TestReason, result);
			this.formatter.Received().WriteSuccess(Resources.TestReason, result);
		}

		[Test]
		public void ShouldVisitItWithFailure()
		{
			var result = new ExampleResult { Status = ExampleResultStatus.Error };
			this.visitor.VisitIt(Resources.TestReason, result);
			this.formatter.Received().WriteError(Resources.TestReason, result);
		}

		[Test]
		public void ShouldVisitDescribe()
		{
			this.visitor.VisitDescribe(Resources.TestReason);
			this.formatter.Received().WriteInformation(Resources.TestReason);
		}
	}
}