namespace NSpec
{
    using System;
    using System.Diagnostics.CodeAnalysis;

    public class Example : ExampleGroup
    {
        public Action BeforeEach { get; set; }

        public Action AfterEach { get; set; }

        [SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Justification = "It will be displayed to the user.")]
        public void It(string reason, Action action)
        {
            this.Visitor.VisitItBeforeEach(reason);
            this.ExampleStrategy.ExecuteAction(this.BeforeEach);

            try
            {
                this.Visitor.VisitIt(
                    reason, 
                    new ExampleResult
                        {
                            Reason = reason, 
                            Status = ExampleResultStatus.Success
                        });
                this.ExampleStrategy.ExecuteAction(action);
            }
            catch (Exception e)
            {
                this.Visitor.VisitIt(
                    reason, 
                    new ExampleResult
                        {
                            Reason = reason, 
                            Exception = e, 
                            Status = ExampleResultStatus.Error
                        });
            }

            this.Visitor.VisitItAfterEach(reason);
            this.ExampleStrategy.ExecuteAction(this.AfterEach);
        }
    }
}