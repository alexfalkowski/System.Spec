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
            this.Describe(
                "describe TestSpecificationWithNestedDescribe1",
                describe => describe.Describe("describe TestSpecificationWithNestedDescribe2", describe2 => { }));
        }
    }
}