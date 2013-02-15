namespace System.Spec
{
    using System;

    public interface IActionStrategy
    {
        void ExecuteAction(Action action);

        void ExecuteAction<T>(Action<T> action, T value);
    }
}