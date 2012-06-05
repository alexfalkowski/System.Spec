namespace NSpec.Specs
{
    using System.Collections.ObjectModel;

    using FluentAssertions;

    using NUnit.Framework;

    [TestFixture]
    public class SpecificationSpecs
    {
        [Test]
        public void ShouldGetMethodsFromAction()
        {
            var list = new Collection<string>();
            var specification = new TestSpecification(list);

            specification.Execute();

            list.Should().ContainInOrder(
                new[] { "describe 1", "before each", "it 1", "before each", "it 2" });
        }
    }
}