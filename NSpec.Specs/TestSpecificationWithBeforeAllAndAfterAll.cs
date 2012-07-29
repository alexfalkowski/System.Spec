﻿namespace NSpec.Specs
{
    public class TestSpecificationWithBeforeAllAndAfterAll : Specification
    {
        public override void Validate()
        {
            this.Describe(
                "describe TestSpecificationWithBeforeAllAndAfterAll",
                // ReSharper disable RedundantArgumentName
                beforeAll: () => { },
                example: describe =>
                    {
                        describe.BeforeEach = () => { };

                        describe.AfterEach = () => { };

                        describe.It("it 1", () => { });
                    },
                afterAll: () => { });
            // ReSharper restore RedundantArgumentName
        }
    }
}