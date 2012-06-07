namespace NSpec.Specs
{
    using System.Collections.Generic;

    internal class TestSpecificationWithSingleIt : TestSpecification
    {
        internal TestSpecificationWithSingleIt(IList<string> list)
            : base(list)
        {
        }

        public override void Validate()
        {
            this.Describe(
                "describe 1",
                example: describe =>
                    {
                        this.List.Add("describe 1");

                        describe.It("it 1", () => this.List.Add("it 1"));
                    });
        }
    }
}