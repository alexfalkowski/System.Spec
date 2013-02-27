namespace System.Spec.Example.Specs
{
    using System.Spec;

	public class TestSpecificationWithSingleIt : Specification
	{
        protected override void Define()
		{
            Describe("describe TestSpecificationWithSingleIt", () => {              
                It("it 1", () => {
                });
            });
		}
	}
}