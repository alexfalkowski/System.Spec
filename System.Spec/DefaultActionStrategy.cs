namespace System.Spec
{
    using System;
    using System.Diagnostics;

	public class DefaultActionStrategy : IActionStrategy
	{
		public long ExecuteAction(Action action)
		{
			if (action == null) {
				return 0;
			}

            var stopwatch = new Stopwatch();
            stopwatch.Start();

			action();

            stopwatch.Stop();
            
            return stopwatch.ElapsedMilliseconds;
		}

		public long ExecuteAction<T>(Action<T> action, T value)
		{
			if (action == null) {
				return 0;
			}

            var stopwatch = new Stopwatch();
            stopwatch.Start();

			action(value);

            stopwatch.Stop();

            return stopwatch.ElapsedMilliseconds;
		}
	}
}