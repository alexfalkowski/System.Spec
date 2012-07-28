namespace NSpec.Specs
{
    using System;

    internal class TestSpecificationWithBeforeAllAndAfterAll : Specification
    {
        internal TestSpecificationWithBeforeAllAndAfterAll(ISpecificationVisitor visitor)
            : base(visitor)
        {
        }

        public override void Validate()
        {
            this.Describe(
                "describe 1",
                // ReSharper disable RedundantArgumentName
                beforeAll: () => Console.WriteLine("before all"),
                example: describe =>
                    {
                        Console.WriteLine("describe 1");

                        describe.BeforeEach = () => Console.WriteLine("before each");

                        describe.AfterEach = () => Console.WriteLine("after each");

                        describe.It("it 1", () => Console.WriteLine("it 1"));
                    },
                afterAll: () => Console.WriteLine("after all"));
            // ReSharper restore RedundantArgumentName
        }
    }
}