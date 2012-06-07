namespace NSpec
{
    using System;

    public class Example : ExampleGroup
    {
        public Action BeforeEach { get; set; }

        public Action AfterEach { get; set; }

        public void It(string reason, Action action)
        {
            this.ReaiseBeforeEach();

            Console.WriteLine(reason);
            action();

            this.RaiseAfterEach();
        }

        private static void RaiseAction(Action action)
        {
            if (action != null)
            {
                action();
            }
        }

        private void RaiseAfterEach()
        {
            var afterEach = this.AfterEach;

            RaiseAction(afterEach);
        }

        private void ReaiseBeforeEach()
        {
            var beforeEach = this.BeforeEach;

            RaiseAction(beforeEach);
        }
    }
}