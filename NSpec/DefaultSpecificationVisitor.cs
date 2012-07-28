namespace NSpec
{
    public class DefaultSpecificationVisitor : ISpecificationVisitor
    {
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
            this.NumberOfExamples++;

            if (result.Status == ExampleResultStatus.Failure)
            {
                this.NumberOfFailures++;
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