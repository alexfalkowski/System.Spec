namespace System.Spec.Example.Specs
{
    using System.Spec;

	public class TestSpecificationWithSingleItWithBeforeEachAndAfterEach : Specification
	{
		public override void Validate()
		{
			this.Describe(
                "describe TestSpecificationWithSingleItWithBeforeEachAndAfterEach",
                describe =>
			{
				describe.BeforeEach = () => { };

				describe.AfterEach = () => { };

				describe.It("it 1", () => { });
			});
		}
	}
}