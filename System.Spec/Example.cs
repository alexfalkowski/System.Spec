namespace System.Spec
{
    using System;

    public class Example : ExampleGroup
    {
        public Action BeforeEach { get; set; }

        public Action AfterEach { get; set; }

        public void It(string reason, Action action)
        {
            this.Visitor.VisitItBeforeEach(reason);
            this.ExampleStrategy.ExecuteAction(this.BeforeEach);

            var actionResult = this.ExampleStrategy.ExecuteActionWithResult(action);
            var result = new ExampleResult { 
                Reason = reason,
                Status = actionResult.Status,
                Exception = actionResult.Exception,
                ElapsedTime = actionResult.ElapsedTime 
            };

            this.Visitor.VisitIt(reason, result);

            this.Visitor.VisitItAfterEach(reason);
            this.ExampleStrategy.ExecuteAction(this.AfterEach);
        }
    }
}