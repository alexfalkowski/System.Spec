namespace NSpec.Specs
{
    using NSubstitute;

    public class TestSpecificationWithNSubstitute : Specification
    {
        public TestSpecificationWithNSubstitute(ISpecificationVisitor visitor)
            : base(visitor)
        {
        }

        private interface ITestInterface
        {
            void TestMethod();
        }

        public override void Validate()
        {
            this.Describe(
                "using NSustitute",
                example: describe => describe.It(
                    "call method",
                    () =>
                        {
                            var testInterface = Substitute.For<ITestInterface>();

                            testInterface.Received().TestMethod();
                        }));
        }
    }
}