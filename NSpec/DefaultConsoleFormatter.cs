namespace NSpec
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;

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

        public void WriteErrors(IEnumerable<ExampleResult> examples)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            var arrayOfExamples = examples.ToArray();

            for (var index = 0; index < arrayOfExamples.Count(); index++)
            {
                var example = arrayOfExamples[index];
                var errorMessage = string.Format(
                    CultureInfo.CurrentCulture, Resources.ConsoleFormatteErrorsMessage, index + 1, example.Exception);

                Console.WriteLine(errorMessage);
            }
        }

        public void WriteSummary(int numberOfExamples, int numberOfFailures, long elapsedMilliseconds)
        {
            Console.ForegroundColor = ConsoleColor.White;

            var elapsdeTimeMessage = string.Format(
                CultureInfo.CurrentCulture, Resources.ConsoleFormatterElapsedTimeMessage, elapsedMilliseconds / 1000L);
            Console.WriteLine(elapsdeTimeMessage);

            var summaryMessage = string.Format(
                CultureInfo.CurrentCulture, Resources.ConsoleFormatterSummaryMessage, numberOfExamples, numberOfFailures);
            Console.WriteLine(summaryMessage);
        }
    }
}