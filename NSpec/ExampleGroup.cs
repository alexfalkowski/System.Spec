namespace NSpec
{
    using System;

    public class ExampleGroup
    {
        private readonly ISpecificationVisitor visitor;

        protected ExampleGroup(ISpecificationVisitor visitor)
        {
            this.visitor = visitor;
        }

        public void Describe(
            string reason, Action beforeAll = null, Action<Example> example = null, Action afterAll = null)
        {
            this.RaiseAction(reason, beforeAll, this.visitor.VisitDescribeBeforeAll);

            if (example != null)
            {
                example(new Example(this.visitor));
                this.visitor.VisitDescribe(reason);
            }

            this.RaiseAction(reason, afterAll, this.visitor.VisitDescribeAfterAll);
        }

        protected void RaiseAction(string reason, Action action, Action<string> visitorAction)
        {
            if (action == null)
            {
                return;
            }

            action();
            visitorAction(reason);
        }
    }
}