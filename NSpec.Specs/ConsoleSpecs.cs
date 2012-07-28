namespace NSpec.Specs
{
    using System.Reflection;

    using FluentAssertions;

    using NUnit.Framework;

    [TestFixture]
    public class ConsoleSpecs
    {
        [Test]
        public void ShouldFindSpecificationsInCurrentlyRunningDll()
        {
            var command = new Command();
            var types = command.GetSpecificationTypes(Assembly.GetExecutingAssembly());
            types.Should().HaveCount(9);
        }
    }
}