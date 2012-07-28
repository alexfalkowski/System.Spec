namespace NSpec
{
    using System;

    public class Example : ExampleGroup
    {
        private readonly ISpecificationVisitor visitor;

        public Example(ISpecificationVisitor visitor)
            : base(visitor)
        {
            this.visitor = visitor;
        }

        public Action BeforeEach { private get; set; }

        public Action AfterEach { private get; set; }

        public void It(string reason, Action action)
        {
            this.RaiseAction(reason, this.BeforeEach, this.visitor.VisitItBeforeEach);

            try
            {
                action();
                this.visitor.VisitIt(reason, new ExampleResult { Status = ExampleResultStatus.Success });
            }
            catch (Exception e)
            {
                this.visitor.VisitIt(reason, new ExampleResult { Exception = e, Status = ExampleResultStatus.Failure });
            }

            this.RaiseAction(reason, this.AfterEach, this.visitor.VisitItAfterEach);
        }
    }
}