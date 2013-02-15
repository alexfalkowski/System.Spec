namespace System.Spec.Specs
{
	public class TestSpecificationWithSingleItWithBeforeEach : Specification
	{
		public override void Validate()
		{
			this.Describe("describe TestSpecificationWithSingleItWithBeforeEach", describe =>
			{
				describe.BeforeEach = () => { };

				describe.It("it 1", () => { });
			});
		}
	}
}