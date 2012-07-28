namespace NSpec.Specs
{
    using System;

    using FluentAssertions;

    using NSubstitute;

    using NUnit.Framework;

    [TestFixture]
    public class SpecificationSpecs
    {
        private ISpecificationVisitor visitor;

        [SetUp]
        public void BeforeEach()
        {
            this.visitor = Substitute.For<ISpecificationVisitor>();
        }

        [Test]
        public void ShouldValidateSingleDescribe()
        {
            var specification = new TestSpecificationWithJustDescribe(this.visitor);

            specification.Validate();

            this.visitor.Received().VisitDescribe("describe 1");
        }

        [Test]
        public void ShouldValidateNestedDescribe()
        {
            var specification = new TestSpecificationWithNestedDescribe(this.visitor);

            specification.Validate();

            this.visitor.Received().VisitDescribe("describe 1");
            this.visitor.Received().VisitDescribe("describe 2");
        }

        [Test]
        public void ShouldValidateSingleDescribeWithSingleIt()
        {
            var specification = new TestSpecificationWithSingleIt(this.visitor);

            specification.Validate();

            this.visitor.Received().VisitDescribe("describe 1");
            this.visitor.Received().VisitIt("it 1");
        }

        [Test]
        public void ShouldValidateSingleDescribeWithSingleItAndBeforeEach()
        {
            var specification = new TestSpecificationWithSingleItWithBeforeEach(this.visitor);

            specification.Validate();

            this.visitor.Received().VisitDescribe("describe 1");
            this.visitor.Received().VisitItBeforeEach("it 1");
            this.visitor.Received().VisitIt("it 1");
        }

        [Test]
        public void ShouldValidateSingleDescribeWithSingleItAndBeforeEachAndAfterEach()
        {
            var specification = new TestSpecificationWithSingleItWithBeforeEachAndAfterEach(this.visitor);

            specification.Validate();

            this.visitor.Received().VisitDescribe("describe 1");
            this.visitor.Received().VisitItBeforeEach("it 1");
            this.visitor.Received().VisitIt("it 1");
            this.visitor.Received().VisitItAfterEach("it 1");
        }

        [Test]
        public void ShouldValidateSpecificationWithBeforeAllAndAfterAll()
        {
            var specification = new TestSpecificationWithBeforeAllAndAfterAll(this.visitor);

            specification.Validate();

            this.visitor.Received().VisitDescribeBeforeAll("describe 1");
            this.visitor.Received().VisitDescribe("describe 1");
            this.visitor.Received().VisitItBeforeEach("it 1");
            this.visitor.Received().VisitIt("it 1");
            this.visitor.Received().VisitItAfterEach("it 1");
            this.visitor.Received().VisitDescribeAfterAll("describe 1");
        }

        [Test]
        public void ShouldValidateWithFluentAsserstions()
        {
            var specification = new TestSpecificationWithFluentAssertions(this.visitor);

            Action action = specification.Validate;

            action.ShouldThrow<Exception>();
        }

        [Test]
        public void ShouldValidateWithNSubstitute()
        {
            var specification = new TestSpecificationWithNSubstitute(this.visitor);

            Action action = specification.Validate;

            action.ShouldThrow<Exception>();
        }
    }
}