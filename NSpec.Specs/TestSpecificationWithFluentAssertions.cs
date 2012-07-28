namespace NSpec.Specs
{
    using FluentAssertions;

    public class TestSpecificationWithFluentAssertions : Specification
    {
        public TestSpecificationWithFluentAssertions(ISpecificationVisitor visitor)
            : base(visitor)
        {
        }

        public override void Validate()
        {
            this.Describe(
                "trying to do an assertion using FluentAssertions",
                describe => describe.It("should be true", () => false.Should().BeTrue()));
        }        
    }
}