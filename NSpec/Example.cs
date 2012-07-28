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

        public Action BeforeEach { get; set; }

        public Action AfterEach { get; set; }

        public void It(string reason, Action action)
        {
            this.RaiseAction(reason, this.BeforeEach, this.visitor.VisitItBeforeEach);

            action();
            this.visitor.VisitIt(reason);

            this.RaiseAction(reason, this.AfterEach, this.visitor.VisitItAfterEach);
        }
    }
}