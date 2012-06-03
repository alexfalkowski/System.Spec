namespace NSpec.Specs
{
    using System;

    using FluentAssertions;

    using NUnit.Framework;

    [TestFixture]
    public class SpecificationSpecs
    {
        [Test]
        public void ShouldGetMethodsFromAction()
        {
            var specification = new Specification();

            Action action = () => specification.Describe("test");
        }

        private class Specification
        {
            public void Describe(string description)
            {
                
            }
        }
    }
}