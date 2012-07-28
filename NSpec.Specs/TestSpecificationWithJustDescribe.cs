namespace NSpec.Specs
{
    using System;

    public class TestSpecificationWithJustDescribe : Specification
    {
        public TestSpecificationWithJustDescribe(ISpecificationVisitor visitor)
            : base(visitor)
        {
        }

        public override void Validate()
        {
            this.Describe("describe 1", example: describe => Console.WriteLine("describe 1"));
        }
    }
}