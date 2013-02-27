namespace System.Spec.Example.Specs
{
    using System.Spec;

	public class TestSpecificationWithBeforeAllAndAfterAll : Specification
	{
		public override void Validate()
		{
			this.Describe("describe TestSpecificationWithBeforeAllAndAfterAll",
                beforeAll: () => { },
                example: describe =>
			{
				describe.BeforeEach = () => { };

				describe.AfterEach = () => { };

				describe.It("it 1", () => { });
			},
                afterAll: () => { });
		}
	}
}