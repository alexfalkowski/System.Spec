namespace NSpec.Specs
{
    using System;

    public class Describe
    {
        public Action BeforeEach { get; set; }

        public void It(string reason, Action action)
        {
            var beforeEach = this.BeforeEach;

            if (beforeEach != null)
            {
                beforeEach();
            }

            Console.WriteLine(reason);
            action();
        } 
    }
}