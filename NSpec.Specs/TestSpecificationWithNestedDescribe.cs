namespace NSpec.Specs
{
    public class TestSpecificationWithNestedDescribe : Specification
    {
        public override void Validate()
        {
            this.Describe(
                "describe TestSpecificationWithNestedDescribe1",
                describe => describe.Describe("describe TestSpecificationWithNestedDescribe2", describe2 => { }));
        }
    }
}