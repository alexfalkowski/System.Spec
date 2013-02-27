namespace System.Spec.Example.Specs
{
    using System.Spec;

    public class TestSpecificationWithJustDescribe : Specification
    {
        protected override void Define()
        {
            Describe("describe TestSpecificationWithJustDescribe", () => {     
            });
        }
    }
}