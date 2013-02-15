namespace System.Spec.Formatter
{
    using System;
    using System.Globalization;
    using System.Linq;

	using System.Spec.Properties;

    public class NestedConsoleFormatter : ConsoleFormatterBase
    {
        public override void WriteInformation(string message)
        {
            WriteWithColour(ConsoleColor.Green, () => Console.WriteLine(message));
        }

        public override void WriteSuccess(string reason, ExampleResult example)
        {
            WriteWithColour(ConsoleColor.Green, () => Console.WriteLine(StringHelper.Tab + reason));

            base.WriteSuccess(reason, example);
        }

        public override void WriteError(string reason, ExampleResult example)
        {
            if (example == null)
            {
                throw new ArgumentNullException("example");
            }

            WriteWithColour(
                ConsoleColor.Red,
                () =>
                Console.WriteLine(
                    string.Format(
                        CultureInfo.CurrentCulture,
                        StringHelper.Tab + Resources.NestedConsoleFormatterErrorMessage,
                        reason,
                        example.Exception)));

            base.WriteError(reason, example);
        }

        public override int WriteSummary(long elapsedMilliseconds)
        {
            if (this.ErrorResults.Any() || this.SuccessResults.Any())
            {
                Console.WriteLine();
            }

            return base.WriteSummary(elapsedMilliseconds);
        }

        // Idea from http://blogs.msdn.com/b/abhinaba/archive/2006/01/05/509581.aspx
        private static void WriteWithColour(ConsoleColor colour, Action action)
        {
            var originalColour = Console.ForegroundColor;

            try
            {
                Console.ForegroundColor = colour;
                action();
            }
            finally
            {
                Console.ForegroundColor = originalColour;
            }
        }
    }
}