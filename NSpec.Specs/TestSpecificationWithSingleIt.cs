namespace NSpec.Specs
{
    using System.Collections.Generic;

    internal class TestSpecificationWithSingleIt : TestSpecification
    {
        internal TestSpecificationWithSingleIt(IList<string> list)
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

                    context.It("it 1", () => this.List.Add("it 1"));
                });
        }
    }
}