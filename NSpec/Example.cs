namespace NSpec
{
    using System;
    using System.Diagnostics.CodeAnalysis;

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

        [SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Justification = "It will be displayed to the user.")]
        public void It(string reason, Action action)
        {
            if (action == null)
            {
                throw new ArgumentNullException("action");
            }

            ExampleGroup.RaiseAction(reason, this.BeforeEach, this.visitor.VisitItBeforeEach);

            try
            {
                action();
                this.visitor.VisitIt(reason, new ExampleResult { Status = ExampleResultStatus.Success });
            }
            catch (Exception e)
            {
                this.visitor.VisitIt(reason, new ExampleResult { Exception = e, Status = ExampleResultStatus.Failure });
            }

            ExampleGroup.RaiseAction(reason, this.AfterEach, this.visitor.VisitItAfterEach);
        }
    }
}