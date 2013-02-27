namespace System.Spec.Example.Specs
{
    using System.Spec;

    public class TestSpecificationWithJustDescribe : Specification
    {
        public override void Validate()
        {
            this.Describe("describe TestSpecificationWithJustDescribe", describe => { });
        }
    }
}