namespace NSpec.Specs
{
    public class TestSpecificationWithJustDescribe : Specification
    {
        public TestSpecificationWithJustDescribe(ISpecificationVisitor visitor)
            : base(visitor)
        {
        }

        public override void Validate()
        {
            this.Describe("describe TestSpecificationWithJustDescribe", describe => { });
        }
    }
}