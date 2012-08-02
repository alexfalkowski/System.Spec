namespace NSpec
{
    public abstract class ExampleBase
    {
        public ISpecificationVisitor Visitor { get; set; }

        public IActionStrategy Strategy { get; set; }

        public IActionStrategy ExampleStrategy { get; set; }
    }
}