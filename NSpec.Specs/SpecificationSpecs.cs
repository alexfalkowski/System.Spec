namespace NSpec.Specs
{
    using System.Collections.ObjectModel;
    using System.Linq;

    using FluentAssertions;

    using NUnit.Framework;

    [TestFixture]
    public class SpecificationSpecs
    {
        private Collection<string> list;

        [SetUp]
        public void BeforeEach()
        {
            this.list = new Collection<string>();
        }

        [Test]
        public void ShouldValidateSingleDescribe()
        {
            var specification = new TestSpecificationWithJustDescribe(this.list);

            specification.Execute();

            this.list.Should().HaveCount(1);
            this.list.First().Should().Be("describe 1");
        }

        [Test]
        public void ShouldValidateSingleDescribeWithSingleIt()
        {
            var specification = new TestSpecificationWithSingleIt(this.list);

            specification.Execute();

            this.list.Should().HaveCount(2);
            this.list[0].Should().Be("describe 1");
            this.list[1].Should().Be("it 1");
        }

        [Test]
        public void ShouldValidateSingleDescribeWithSingleItAndBeforeEach()
        {
            var specification = new TestSpecificationWithSingleItWithBeforeEach(this.list);

            specification.Execute();

            this.list.Should().HaveCount(3);
            this.list[0].Should().Be("describe 1");
            this.list[1].Should().Be("before each");
            this.list[2].Should().Be("it 1");
        }

        [Test]
        public void ShouldValidateSingleDescribeWithSingleItAndBeforeEachAndAfterEach()
        {
            var specification = new TestSpecificationWithSingleItWithBeforeEachAndAfterEach(this.list);

            specification.Execute();

            this.list.Should().HaveCount(4);
            this.list[0].Should().Be("describe 1");
            this.list[1].Should().Be("before each");
            this.list[2].Should().Be("it 1");
            this.list[3].Should().Be("after each");
        }
    }
}