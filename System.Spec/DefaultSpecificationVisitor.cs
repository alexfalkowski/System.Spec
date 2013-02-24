namespace System.Spec
{
    using System;

	using System.Spec.Formatter;

	public class DefaultSpecificationVisitor : ISpecificationVisitor
	{
		private readonly IConsoleFormatter formatter;

		public DefaultSpecificationVisitor(IConsoleFormatter formatter)
		{
			this.formatter = formatter;
		}

		public void VisitDescribe(string reason)
		{
			this.formatter.WriteInformation(reason);
		}

		public void VisitDescribeBeforeAll(string reason)
		{
		}

		public void VisitDescribeAfterAll(string reason)
		{
		}

		public void VisitIt(string reason, ExampleResult result)
		{
			if (result == null) {
				throw new ArgumentNullException("result");
			}

			if (result.Status == ResultStatus.Error) {
				this.formatter.WriteError(reason, result);
			} else {
				this.formatter.WriteSuccess(reason, result);
			}
		}

		public void VisitItBeforeEach(string reason)
		{
		}

		public void VisitItAfterEach(string reason)
		{
		}
	}
}