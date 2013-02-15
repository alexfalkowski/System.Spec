namespace System.Spec.Specs
{
    public class TestSpecificationWithBeforeAll : Specification
    {
        public override void Validate()
        {
            this.Describe(
                "describe TestSpecificationWithBeforeAll",
                // ReSharper disable RedundantArgumentName
                beforeAll: () => { },
                example: describe =>
                    {
                        describe.BeforeEach = () => { };

                        describe.AfterEach = () => { };

                        describe.It("it 1", () => { });
                    });
            // ReSharper restore RedundantArgumentName
        }
    }
}