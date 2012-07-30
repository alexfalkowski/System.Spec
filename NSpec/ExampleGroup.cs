namespace NSpec
{
    using System;
    using System.Diagnostics.CodeAnalysis;

    public class ExampleGroup
    {
        public ISpecificationVisitor Visitor { get; set; }

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
            RaiseAction(reason, beforeAll, this.Visitor.VisitDescribeBeforeAll);

            if (example != null)
            {
                this.Visitor.VisitDescribe(reason);
                example(new Example { Visitor = this.Visitor });
            }

            RaiseAction(reason, afterAll, this.Visitor.VisitDescribeAfterAll);
        }

        [SuppressMessage("Microsoft.Design", "CA1030:UseEventsWhereAppropriate", Justification = "Better using an action.")]
        internal static void RaiseAction(string reason, Action action, Action<string> visitorAction)
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