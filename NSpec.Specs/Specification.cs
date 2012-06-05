namespace NSpec.Specs
{
    using System;

    public abstract class Specification
    {
        public abstract void Execute();

        protected static void Describe(string reason, Action<Describe> action)
        {
            Console.WriteLine(reason);
            action(new Describe());
        }
    }
}