namespace NSpec
{
    using System;

    public class NullActionStrategy : IActionStrategy
    {
        public void ExecuteAction(Action action)
        {
        }

        public void ExecuteAction<T>(Action<T> action, T value)
        {
        }
    }
}