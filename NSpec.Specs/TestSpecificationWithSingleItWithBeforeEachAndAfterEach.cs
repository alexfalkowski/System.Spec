namespace NSpec.Specs
{
    using System;

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
                        Console.WriteLine("describe 1");

                        describe.BeforeEach = () => Console.WriteLine("before each");

                        describe.AfterEach = () => Console.WriteLine("after each");

                        describe.It("it 1", () => Console.WriteLine("it 1"));
                    });
        }
    }
}