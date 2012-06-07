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
                context =>
                    {
                        this.List.Add("describe 1");

                        context.Describe("describe 2", example => this.List.Add("describe 2"));
                    });
        }
    }
}