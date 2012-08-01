namespace NSpec
{
    using System;

    public class DefaultActionStrategy : IActionStrategy
    {
        public void ExecuteAction(Action action)
        {
            if (action == null)
            {
                return;
            }

            action();
        }

        public void ExecuteAction<T>(Action<T> action, T value)
        {
            if (action == null)
            {
                return;
            }

            action(value);
        }
    }
}