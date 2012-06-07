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

        public override void Execute()
        {
            this.Describe(
                "describe 1",
                context =>
                    {
                        this.List.Add("describe 1");

                        context.BeforeEach = () =>
                            {
                                Console.WriteLine("before each");
                                this.List.Add("before each");
                            };

                        context.AfterEach = () =>
                            {
                                Console.WriteLine("after each");
                                this.List.Add("after each");
                            };

                        context.It("it 1", () => this.List.Add("it 1"));
                    });
        }
    }
}