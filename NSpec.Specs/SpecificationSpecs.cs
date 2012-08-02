namespace NSpec.Specs
{
    using NSubstitute;

    using NUnit.Framework;

    [TestFixture]
    public class SpecificationSpecs
    {
        private ISpecificationVisitor visitor;

        private IActionStrategy strategy;

        [SetUp]
        public void BeforeEach()
        {
            this.visitor = Substitute.For<ISpecificationVisitor>();
            this.strategy = new DefaultActionStrategy();
        }

        [Test]
        public void ShouldValidateSingleDescribe()
        {
            var specification = new TestSpecificationWithJustDescribe
                { Visitor = this.visitor, ExampleGroupStrategy = this.strategy };

            specification.Validate();

            this.visitor.Received().VisitDescribe("describe TestSpecificationWithJustDescribe");
        }

        [Test]
        public void ShouldValidateNestedDescribe()
        {
            var specification = new TestSpecificationWithNestedDescribe
                { Visitor = this.visitor, ExampleGroupStrategy = this.strategy };

            specification.Validate();

            this.visitor.Received().VisitDescribe("describe TestSpecificationWithNestedDescribe1");
            this.visitor.Received().VisitDescribe("describe TestSpecificationWithNestedDescribe2");
        }

        [Test]
        public void ShouldValidateSingleDescribeWithSingleIt()
        {
            var specification = new TestSpecificationWithSingleIt
                { Visitor = this.visitor, ExampleGroupStrategy = this.strategy, ExampleStrategy = this.strategy };

            specification.Validate();

            this.visitor.Received().VisitDescribe("describe TestSpecificationWithSingleIt");
            this.visitor.Received().VisitIt("it 1", new ExampleResult { Status = ExampleResultStatus.Success });
        }

        [Test]
        public void ShouldValidateSingleDescribeWithSingleItAndBeforeEach()
        {
            var specification = new TestSpecificationWithSingleItWithBeforeEach
                { Visitor = this.visitor, ExampleGroupStrategy = this.strategy, ExampleStrategy = this.strategy };

            specification.Validate();

            this.visitor.Received().VisitDescribe("describe TestSpecificationWithSingleItWithBeforeEach");
            this.visitor.Received().VisitItBeforeEach("it 1");
            this.visitor.Received().VisitIt("it 1", new ExampleResult { Status = ExampleResultStatus.Success });
        }

        [Test]
        public void ShouldValidateSingleDescribeWithSingleItAndBeforeEachAndAfterEach()
        {
            var specification = new TestSpecificationWithSingleItWithBeforeEachAndAfterEach
                { Visitor = this.visitor, ExampleGroupStrategy = this.strategy, ExampleStrategy = this.strategy };

            specification.Validate();

            this.visitor.Received().VisitDescribe("describe TestSpecificationWithSingleItWithBeforeEachAndAfterEach");
            this.visitor.Received().VisitItBeforeEach("it 1");
            this.visitor.Received().VisitIt("it 1", new ExampleResult { Status = ExampleResultStatus.Success });
            this.visitor.Received().VisitItAfterEach("it 1");
        }

        [Test]
        public void ShouldValidateSpecificationWithBeforeAll()
        {
            var specification = new TestSpecificationWithBeforeAll
                { Visitor = this.visitor, ExampleGroupStrategy = this.strategy, ExampleStrategy = this.strategy };

            specification.Validate();

            this.visitor.Received().VisitDescribeBeforeAll("describe TestSpecificationWithBeforeAll");
            this.visitor.Received().VisitDescribe("describe TestSpecificationWithBeforeAll");
            this.visitor.Received().VisitItBeforeEach("it 1");
            this.visitor.Received().VisitIt("it 1", new ExampleResult { Status = ExampleResultStatus.Success });
            this.visitor.Received().VisitItAfterEach("it 1");
        }

        [Test]
        public void ShouldValidateSpecificationWithBeforeAllAndAfterAll()
        {
            var specification = new TestSpecificationWithBeforeAllAndAfterAll
                { Visitor = this.visitor, ExampleGroupStrategy = this.strategy, ExampleStrategy = this.strategy };

            specification.Validate();

            this.visitor.Received().VisitDescribeBeforeAll("describe TestSpecificationWithBeforeAllAndAfterAll");
            this.visitor.Received().VisitDescribe("describe TestSpecificationWithBeforeAllAndAfterAll");
            this.visitor.Received().VisitItBeforeEach("it 1");
            this.visitor.Received().VisitIt("it 1", new ExampleResult { Status = ExampleResultStatus.Success });
            this.visitor.Received().VisitItAfterEach("it 1");
            this.visitor.Received().VisitDescribeAfterAll("describe TestSpecificationWithBeforeAllAndAfterAll");
        }

        [Test]
        public void ShouldValidateWithFluentAssertions()
        {
            var specification = new TestSpecificationWithFluentAssertions
                { Visitor = this.visitor, ExampleGroupStrategy = this.strategy, ExampleStrategy = this.strategy };

            specification.Validate();

            this.visitor.Received().VisitDescribe("trying to do an assertion using FluentAssertions");
            this.visitor.Received().VisitIt(
                "should be true", new ExampleResult { Status = ExampleResultStatus.Error });
        }

        [Test]
        public void ShouldValidateWithNSubstitute()
        {
            var specification = new TestSpecificationWithNSubstitute
                { Visitor = this.visitor, ExampleGroupStrategy = this.strategy, ExampleStrategy = this.strategy };

            specification.Validate();

            this.visitor.Received().VisitDescribe("using NSustitute");
            this.visitor.Received().VisitIt("call method", new ExampleResult { Status = ExampleResultStatus.Error });
        }
    }
}