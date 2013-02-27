namespace System.Spec.Example.Specs
{
    using System.Spec;

	public class TestSpecificationWithNestedDescribe : Specification
	{
        protected override void Define()
		{
            Describe("describe TestSpecificationWithNestedDescribe1", () => {
                Describe("describe TestSpecificationWithNestedDescribe2", () => {     
                });
            });
		}
	}
}