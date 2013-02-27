namespace System.Spec.Example.Specs
{
    using System.Spec;

	public class TestSpecificationWithSingleItWithBeforeEachAndAfterEach : Specification
	{
        protected override void Define()
		{
            Describe("describe TestSpecificationWithSingleItWithBeforeEachAndAfterEach", () => {
                BeforeEach(() => {
                });

                AfterEach(() => {
                });

                It("it 1", () => {
                });
            });
		}
	}
}