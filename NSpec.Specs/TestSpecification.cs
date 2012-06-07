namespace NSpec.Specs
{
    using System.Collections.Generic;

    public abstract class TestSpecification : Specification
    {
        private readonly IList<string> list;

        protected TestSpecification(IList<string> list)
        {
            this.list = list;
        }

        protected IList<string> List
        {
            get
            {
                return this.list;
            }
        }
    }
}