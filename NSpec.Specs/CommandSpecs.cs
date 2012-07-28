namespace NSpec.Specs
{
    using System.Reflection;

    using FluentAssertions;

    using NUnit.Framework;

    [TestFixture]
    public class CommandSpecs
    {
        private ICommand command;

        private ISpecificationVisitor specificationVisitor;

        [SetUp]
        public void BeforeEach()
        {
            this.specificationVisitor = new DefaultSpecificationVisitor();
            this.command = new DefaultCommand(this.specificationVisitor);
        }

        [Test]
        public void ShouldFindSpecificationsInCurrentlyRunningDll()
        {
            var types = this.command.GetSpecificationTypes(Assembly.GetExecutingAssembly());
            types.Should().HaveCount(8);
        }

        [Test]
        public void ShouldExecuteAllSpecifications()
        {
            var types = this.command.GetSpecificationTypes(Assembly.GetExecutingAssembly());

            this.command.ExecuteSpecifications(types);
            this.specificationVisitor.NumberOfExamples.Should().Be(6);
            this.specificationVisitor.NumberOfFailures.Should().Be(2);
        }
    }
}