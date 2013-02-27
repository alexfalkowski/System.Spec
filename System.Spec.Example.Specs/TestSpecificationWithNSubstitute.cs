namespace System.Spec.Example.Specs
{
    using System.Spec;

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