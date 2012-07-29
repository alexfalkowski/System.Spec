namespace NSpec.Specs
{
    public class TestSpecificationWithSingleItWithBeforeEach : Specification
    {
        public TestSpecificationWithSingleItWithBeforeEach(ISpecificationVisitor visitor)
            : base(visitor)
        {
        }

        public override void Validate()
        {
            this.Describe(
                "describe TestSpecificationWithSingleItWithBeforeEach",
                describe =>
                    {
                        describe.BeforeEach = () => { };

                        describe.It("it 1", () => { });
                    });
        }
    }
}