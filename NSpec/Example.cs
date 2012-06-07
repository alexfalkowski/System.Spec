namespace NSpec
{
    using System;

    public class Example : ExampleGroup
    {
        public Action BeforeEach { get; set; }

        public Action AfterEach { get; set; }

        public void It(string reason, Action action)
        {
            this.RaiseAction(this.BeforeEach);

            Console.WriteLine(reason);
            action();

            this.RaiseAction(this.AfterEach);
        }
    }
}