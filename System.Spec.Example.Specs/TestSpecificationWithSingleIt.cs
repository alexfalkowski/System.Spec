namespace System.Spec.Example.Specs
{
    using System.Spec;

	public class TestSpecificationWithSingleIt : Specification
	{
		public override void Validate()
		{
			this.Describe(
                "describe TestSpecificationWithSingleIt", describe => describe.It("it 1", () => { }));
		}
	}
}