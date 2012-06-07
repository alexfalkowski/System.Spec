namespace NSpec
{
    using System;

    public class ExampleGroup
    {
        public void Describe(
            string reason, Action beforeAll = null, Action<Example> example = null, Action afterAll = null)
        {
            this.RaiseAction(beforeAll);

            Console.WriteLine(reason);

            if (example != null)
            {
                example(new Example());
            }

            this.RaiseAction(afterAll);
        }

        protected void RaiseAction(Action action)
        {
            if (action != null)
            {
                action();
            }
        }
    }
}