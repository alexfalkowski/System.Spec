namespace NSpec.Specs
{
    public class TestSpecificationWithSingleItWithBeforeEachAndAfterEach : Specification
    {
        public TestSpecificationWithSingleItWithBeforeEachAndAfterEach(ISpecificationVisitor visitor)
            : base(visitor)
        {
        }

        public override void Validate()
        {
            this.Describe(
                "describe 1",
                example: describe =>
                    {
                        describe.BeforeEach = () => { };

                        describe.AfterEach = () => { };

                        describe.It("it 1", () => { });
                    });
        }
    }
}