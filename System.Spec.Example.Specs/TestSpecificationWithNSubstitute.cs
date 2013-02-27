namespace System.Spec.Example.Specs
{
    using System.Spec;

    using NSubstitute;

	public class TestSpecificationWithNSubstitute : Specification
	{
        protected override void Define()
		{
            Describe("using NSustitute", () => {             
                It("call method", () => {
                    var testInterface = Substitute.For<ITestInterface>();
                    
                    testInterface.Received().TestMethod();
                });
            });
		}
	}
}