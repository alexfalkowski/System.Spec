namespace NSpec.Specs
{
    using System;
    using System.Reflection;

    using FluentAssertions;

    using NSubstitute;

    using NUnit.Framework;

    [TestFixture]
    public class CommandSpecs
    {
        private ICommand command;

        [SetUp]
        public void BeforeEach()
        {
            this.command = new DefaultCommand(Substitute.For<ISpecificationVisitor>());
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

            Action action = () => this.command.ExecuteSpecifications(types);
            action.ShouldNotThrow();
        }
    }
}