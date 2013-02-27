namespace System.Spec.Example.Specs
{
    using System.Spec;

	public class TestSpecificationWithSingleItWithBeforeEach : Specification
	{
        protected override void Define()
		{
            Describe("describe TestSpecificationWithSingleItWithBeforeEach", () => {
                BeforeEach(() => {
                });
                
                It("it 1", () => {
                });
            });
		}
	}
}