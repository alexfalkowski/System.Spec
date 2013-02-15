namespace System.Spec
{
    using System;
	using System.Diagnostics.CodeAnalysis;

	public class Example : ExampleGroup
	{
		public Action BeforeEach { get; set; }

		public Action AfterEach { get; set; }

		[SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Justification = "It will be displayed to the user.")]
		public void It(string reason, Action action)
		{
			this.Visitor.VisitItBeforeEach(reason);
			this.ExampleStrategy.ExecuteAction(this.BeforeEach);
			ExampleResult result;

			try {
				this.ExampleStrategy.ExecuteAction(action);
				result = new ExampleResult { Reason = reason, Status = ExampleResultStatus.Success };
			} catch (Exception e) {
				result = new ExampleResult { Reason = reason, Exception = e, Status = ExampleResultStatus.Error };
			}

			this.Visitor.VisitIt(reason, result);

			this.Visitor.VisitItAfterEach(reason);
			this.ExampleStrategy.ExecuteAction(this.AfterEach);
		}
	}
}