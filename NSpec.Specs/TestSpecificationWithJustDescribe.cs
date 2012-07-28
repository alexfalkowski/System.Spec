namespace NSpec.Specs
{
    using System;

    internal class TestSpecificationWithJustDescribe : Specification
    {
        internal TestSpecificationWithJustDescribe(ISpecificationVisitor visitor)
            : base(visitor)
        {
        }

        public override void Validate()
        {
            this.Describe("describe 1", example: describe => Console.WriteLine("describe 1"));
        }
    }
}