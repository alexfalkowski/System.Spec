namespace System.Spec.Specs
{
    public class TestSpecificationWithSingleIt : Specification
    {
        public override void Validate()
        {
            this.Describe(
                "describe TestSpecificationWithSingleIt", describe => describe.It("it 1", () => { }));
        }
    }
}