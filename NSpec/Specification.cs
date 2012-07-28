namespace NSpec
{
    public abstract class Specification : ExampleGroup
    {
        protected Specification(ISpecificationVisitor visitor)
            : base(visitor)
        {
        }

        public abstract void Validate();
    }
}