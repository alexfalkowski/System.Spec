namespace System.Spec.Example.Specs
{
    using System.Spec;

	public class TestSpecificationWithBeforeAllAndAfterAll : Specification
	{
        protected override void Define()
		{
            Describe("describe TestSpecificationWithBeforeAllAndAfterAll", () => {
                BeforeAll(() => {
                });

                AfterAll(() => {
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