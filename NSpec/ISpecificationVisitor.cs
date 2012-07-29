namespace NSpec
{
    using System.Collections.Generic;

    public interface ISpecificationVisitor
    {
        int NumberOfExamples { get; }

        IEnumerable<ExampleResult> NumberOfFailures { get; }

        void VisitDescribe(string reason);

        void VisitDescribeBeforeAll(string reason);

        void VisitDescribeAfterAll(string reason);

        void VisitIt(string reason, ExampleResult result);

        void VisitItBeforeEach(string reason);

        void VisitItAfterEach(string reason);
    }
}