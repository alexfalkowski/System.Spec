namespace System.Spec
{
    using System;

    public class NullActionStrategy : IActionStrategy
    {
        public long ExecuteAction(Action action)
        {
            return 0;
        }

        public long ExecuteAction<T>(Action<T> action, T value)
        {
            return 0;
        }

        public ActionResult ExecuteActionWithResult(Action action)
        {
            return new ActionResult {Status = ResultStatus.Success };
        }
    }
}