namespace System.Spec.Specs
{
    using NSubstitute;

	public class TestSpecificationWithNSubstitute : Specification
	{
		public override void Validate()
		{
			this.Describe(
                "using NSustitute",
                describe => describe.It(
                    "call method",
                    () =>
			{
				var testInterface = Substitute.For<ITestInterface>();

				testInterface.Received().TestMethod();
			}));
		}
	}
}