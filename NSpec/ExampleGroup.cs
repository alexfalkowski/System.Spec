namespace NSpec
{
    using System;

    public class ExampleGroup
    {
        public void Describe(string reason, Action beforeAll = null, Action<Example> example = null, Action afterAll = null)
        {
            if (beforeAll != null)
            {
                beforeAll();
            }

            Console.WriteLine(reason);

            if (example != null)
            {
                example(new Example());
            }

            if (afterAll != null)
            {
                afterAll();
            }
        }
    }
}