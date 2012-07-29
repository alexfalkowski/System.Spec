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

        public void WriteSummary(ISpecificationVisitor visitor)
        {
            if (visitor == null)
            {
                throw new ArgumentNullException("visitor");
            }

            Console.ForegroundColor = ConsoleColor.White;
            var message = string.Format(
                CultureInfo.CurrentCulture,
                Resources.ConsoleFormatterSummaryMessage,
                visitor.NumberOfExamples,
                visitor.NumberOfFailures);
            Console.Write(message);
        }
    }
}