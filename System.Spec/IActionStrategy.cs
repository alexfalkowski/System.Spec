namespace System.Spec
{
    using System;

    public interface IActionStrategy
    {
        long ExecuteAction(Action action);

        long ExecuteAction<T>(Action<T> action, T value);

        ActionResult ExecuteActionWithResult(Action action);
    }
}