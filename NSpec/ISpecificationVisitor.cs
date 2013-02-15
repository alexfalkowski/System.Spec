namespace System.Spec
{
    using System;

    public interface ISpecificationVisitor
    {
        void VisitDescribe(string reason);

        void VisitDescribeBeforeAll(string reason);

        void VisitDescribeAfterAll(string reason);

        void VisitIt(string reason, ExampleResult result);

        void VisitItBeforeEach(string reason);

        void VisitItAfterEach(string reason);
    }
}