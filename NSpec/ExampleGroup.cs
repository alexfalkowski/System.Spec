namespace NSpec
{
    using System;
    using System.Diagnostics.CodeAnalysis;

    public class ExampleGroup
    {
        private readonly ISpecificationVisitor visitor;

        protected ExampleGroup(ISpecificationVisitor visitor)
        {
            this.visitor = visitor;
        }

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
            RaiseAction(reason, beforeAll, this.visitor.VisitDescribeBeforeAll);

            if (example != null)
            {
                example(new Example(this.visitor));
                this.visitor.VisitDescribe(reason);
            }

            RaiseAction(reason, afterAll, this.visitor.VisitDescribeAfterAll);
        }

        [SuppressMessage("Microsoft.Design", "CA1030:UseEventsWhereAppropriate", Justification = "Better using an action.")]
        protected static void RaiseAction(string reason, Action action, Action<string> visitorAction)
        {
            if (visitorAction == null)
            {
                throw new ArgumentNullException("visitorAction");
            }

            if (action == null)
            {
                return;
            }

            action();
            visitorAction(reason);
        }
    }
}