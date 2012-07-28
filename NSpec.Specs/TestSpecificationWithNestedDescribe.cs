namespace NSpec.Specs
{
    public class TestSpecificationWithNestedDescribe : Specification
    {
        public TestSpecificationWithNestedDescribe(ISpecificationVisitor visitor)
            : base(visitor)
        {
        }

        public override void Validate()
        {
            this.Describe("describe 1", describe => describe.Describe("describe 2", describe2 => { }));
        }
    }
}