namespace NSpec.Specs
{
    using System;

    public class TestSpecificationWithSingleIt : Specification
    {
        public TestSpecificationWithSingleIt(ISpecificationVisitor visitor)
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

                        describe.It("it 1", () => Console.WriteLine("it 1"));
                    });
        }
    }
}