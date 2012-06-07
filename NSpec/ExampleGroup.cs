namespace NSpec
{
    using System;

    public class ExampleGroup
    {
        public void Describe(string reason, Action<Example> action)
        {
            Console.WriteLine(reason);
            action(new Example());
        } 
    }
}