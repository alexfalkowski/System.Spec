namespace NSpec
{
    using System;

    public class ExampleGroup : ExampleBase
    {
        public void Describe(string reason, Action<Example> example)
        {
            this.Describe(reason, null, example, null);
        }

        public void Describe(string reason, Action beforeAll, Action<Example> example)
        {
            this.Describe(reason, beforeAll, example, null);
        }

        public void Describe(
            string reason, Action beforeAll, Action<Example> example, Action afterAll)
        {
            this.Visitor.VisitDescribeBeforeAll(reason);
            this.Strategy.ExecuteAction(beforeAll);

            this.Visitor.VisitDescribe(reason);
            this.Strategy.ExecuteAction(
                example,
                new Example
                    {
                        Visitor = this.Visitor,
                        Strategy = this.Strategy,
                        ExampleStrategy = this.ExampleStrategy, 
                    });

            this.Visitor.VisitDescribeAfterAll(reason);
            this.Strategy.ExecuteAction(afterAll);
        }
    }
}