namespace System.Spec.Example.Specs
{
    using System.Spec;

	public class TestSpecificationWithBeforeAll : Specification
	{
		protected override void Define()
		{
            Describe("describe TestSpecificationWithBeforeAll", () => {
                BeforeAll(() => {
                });

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