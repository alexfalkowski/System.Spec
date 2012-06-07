namespace NSpec.Specs
{
    using System;
    using System.Collections.Generic;

    internal class TestSpecificationWithBeforeAllAndAfterAll : TestSpecification
    {
        internal TestSpecificationWithBeforeAllAndAfterAll(IList<string> list)
            : base(list)
        {
        }

        public override void Execute()
        {
            this.Describe(
                "describe 1",
                // ReSharper disable RedundantArgumentName
                beforeAll: () =>
                    {
                        Console.WriteLine("before all");
                        this.List.Add("before all");
                    },
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
                    },
                afterAll: () =>
                    {
                        Console.WriteLine("after all");
                        this.List.Add("after all");
                    });
            // ReSharper restore RedundantArgumentName
        }
    }
}