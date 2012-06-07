namespace NSpec.Specs
{
    using System;
    using System.Collections.Generic;

    public class TestSpecificationWithSingleItWithBeforeEachAndAfterEach : TestSpecification
    {
        public TestSpecificationWithSingleItWithBeforeEachAndAfterEach(IList<string> list)
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

                        describe.BeforeEach = () =>
                            {
                                Console.WriteLine("before each");
                                this.List.Add("before each");
                            };

                        describe.AfterEach = () =>
                            {
                                Console.WriteLine("after each");
                                this.List.Add("after each");
                            };

                        describe.It("it 1", () => this.List.Add("it 1"));
                    });
        }
    }
}