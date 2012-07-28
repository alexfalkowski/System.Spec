namespace NSpec.Specs
{
    using System;

    internal class TestSpecificationWithNestedDescribe : Specification
    {
        internal TestSpecificationWithNestedDescribe(ISpecificationVisitor visitor)
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

                        describe.Describe("describe 2", example: describe2 => Console.WriteLine("describe 2"));
                    });
        }
    }
}