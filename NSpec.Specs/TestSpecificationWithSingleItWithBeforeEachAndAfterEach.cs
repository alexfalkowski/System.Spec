namespace NSpec.Specs
{
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