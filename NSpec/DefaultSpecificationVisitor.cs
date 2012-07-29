namespace NSpec
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;

    public class DefaultSpecificationVisitor : ISpecificationVisitor
    {
        private readonly IConsoleFormatter formatter;

        private readonly IList<ExampleResult> numberOfFailures = new Collection<ExampleResult>();

        public DefaultSpecificationVisitor(IConsoleFormatter formatter)
        {
            this.formatter = formatter;
        }

        public int NumberOfExamples { get; private set; }

        public IEnumerable<ExampleResult> NumberOfFailures
        {
            get
            {
                return this.numberOfFailures;
            }
        }

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
                this.numberOfFailures.Add(result);
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