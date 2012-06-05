namespace NSpec.Specs
{
    using System;
    using System.Collections.Generic;

    internal class TestSpecification : Specification
    {
        private readonly IList<string> list;

        public TestSpecification(IList<string> list)
        {
            this.list = list;
        }

        public override void Execute()
        {
            Describe(
                "describe 1",
                context =>
                    {
                        this.list.Add("describe 1");

                        context.BeforeEach = () =>
                            {
                                Console.WriteLine("before each");
                                this.list.Add("before each");
                            };

                        context.It("it 1", () => this.list.Add("it 1"));

                        context.It("it 2", () => this.list.Add("it 2"));
                    });
        }
    }
}