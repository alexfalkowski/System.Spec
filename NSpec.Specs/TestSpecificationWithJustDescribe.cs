namespace NSpec.Specs
{
    using System.Collections.Generic;

    internal class TestSpecificationWithJustDescribe : TestSpecification
    {
        internal TestSpecificationWithJustDescribe(IList<string> list)
            : base(list)
        {
        }

        public override void Execute()
        {
            this.Describe("describe 1", example: describe => this.List.Add("describe 1"));
        }
    }
}