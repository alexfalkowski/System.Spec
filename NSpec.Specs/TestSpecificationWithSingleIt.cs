namespace NSpec.Specs
{
    public class TestSpecificationWithSingleIt : Specification
    {
        public TestSpecificationWithSingleIt(ISpecificationVisitor visitor)
            : base(visitor)
        {
        }

        public override void Validate()
        {
            this.Describe(
                "describe TestSpecificationWithSingleIt", describe => describe.It("it 1", () => { }));
        }
    }
}