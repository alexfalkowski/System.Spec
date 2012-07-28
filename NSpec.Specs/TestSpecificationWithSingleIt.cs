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
                "describe 1", describe => describe.It("it 1", () => { }));
        }
    }
}