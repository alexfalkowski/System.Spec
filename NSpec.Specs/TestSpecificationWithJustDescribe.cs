namespace NSpec.Specs
{
    public class TestSpecificationWithJustDescribe : Specification
    {
        public override void Validate()
        {
            this.Describe("describe TestSpecificationWithJustDescribe", describe => { });
        }
    }
}