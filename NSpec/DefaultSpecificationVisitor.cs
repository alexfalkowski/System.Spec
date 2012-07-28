namespace NSpec
{
    using System;

    public class DefaultSpecificationVisitor : ISpecificationVisitor
    {
        private readonly IConsoleFormatter formatter;

        public DefaultSpecificationVisitor(IConsoleFormatter formatter)
        {
            this.formatter = formatter;
        }

        public int NumberOfExamples { get; private set; }

        public int NumberOfFailures { get; private set; }

        public void VisitDescribe(string reason)
        {
        }

        public void VisitDescribeBeforeAll(string reason)
        {
        }

        public void VisitDescribeAfterAll(string reason)
        {
        }

        public void VisitIt(string reason, ExampleResult result)
        {
            if (result == null)
            {
                throw new ArgumentNullException("result");
            }

            this.NumberOfExamples++;

            if (result.Status == ExampleResultStatus.Failure)
            {
                this.NumberOfFailures++;
                this.formatter.WriteError();
            }
            else
            {
                this.formatter.WriteSuccess();
            }
        }

        public void VisitItBeforeEach(string reason)
        {
        }

        public void VisitItAfterEach(string reason)
        {
        }
    }
}