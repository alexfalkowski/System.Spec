namespace NSpec
{
    public abstract class ExampleBase
    {
        public ISpecificationVisitor Visitor { get; set; }

        public IActionStrategy ExampleGroupStrategy { get; set; }

        public IActionStrategy ExampleStrategy { get; set; }
    }
}