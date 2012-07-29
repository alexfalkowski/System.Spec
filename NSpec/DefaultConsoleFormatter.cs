namespace NSpec
{
    using System;
    using System.Globalization;

    using NSpec.Properties;

    public class DefaultConsoleFormatter : IConsoleFormatter
    {
        public void WriteSuccess()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(Resources.ConsoleFormatterSuccessMessage);
        }

        public void WriteError()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(Resources.ConsoleFormatterErrorMessage);
        }

        public void WriteSummary(ISpecificationVisitor visitor, long elapsedMilliseconds)
        {
            if (visitor == null)
            {
                throw new ArgumentNullException("visitor");
            }

            Console.ForegroundColor = ConsoleColor.White;

            var elapsdeTimeMessage = string.Format(
                CultureInfo.CurrentCulture, Resources.ConsoleFormatterElapsedTimeMessage, elapsedMilliseconds / 1000L);
            Console.WriteLine(elapsdeTimeMessage);

            var summaryMessage = string.Format(
                CultureInfo.CurrentCulture,
                Resources.ConsoleFormatterSummaryMessage,
                visitor.NumberOfExamples,
                visitor.NumberOfFailures);
            Console.WriteLine(summaryMessage);
        }
    }
}