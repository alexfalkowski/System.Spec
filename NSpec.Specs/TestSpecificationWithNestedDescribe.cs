namespace NSpec.Specs
{
    using System.Collections.Generic;

    internal class TestSpecificationWithNestedDescribe : TestSpecification
    {
        internal TestSpecificationWithNestedDescribe(IList<string> list)
            : base(list)
        {
        }

        public override void Execute()
        {
            this.Describe(
                "describe 1",
                example: describe =>
                    {
                        this.List.Add("describe 1");

                        describe.Describe("describe 2", example: describe2 => this.List.Add("describe 2"));
                    });
        }
    }
}