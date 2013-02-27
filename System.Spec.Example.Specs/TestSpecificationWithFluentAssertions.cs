namespace System.Spec.Example.Specs
{
    using System.Spec;
    using FluentAssertions;

    public class TestSpecificationWithFluentAssertions : Specification
    {
        protected override void Define()
        {
            Describe("trying to do an assertion using FluentAssertions", () => {     
                It("should be true", () => {
                    false.Should().BeTrue();
                });
            });
        }        
    }
}