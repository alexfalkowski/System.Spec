namespace NSpec
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;

    using NSpec.Properties;

    public class ProgressConsoleFormatter : IConsoleFormatter
    {
        public void WriteSuccess()
        {
            Console.Write(Resources.ConsoleFormatterSuccessMessage);
        }

        public void WriteError()
        {
            Console.Write(Resources.ConsoleFormatterErrorMessage);
        }

        public void WriteErrors(IEnumerable<ExampleResult> examples)
        {
            var arrayOfExamples = examples.ToArray();

            if (arrayOfExamples.Length <= 0)
            {
                return;
            }

            Console.WriteLine(Environment.NewLine);

            for (var index = 0; index < arrayOfExamples.Count(); index++)
            {
                var example = arrayOfExamples[index];
                var errorMessage = string.Format(
                    CultureInfo.CurrentCulture,
                    Resources.ConsoleFormatteErrorsMessage,
                    index + 1,
                    example.Reason,
                    example.Exception);

                Console.WriteLine(errorMessage);
                Console.WriteLine();
            }
        }

        public void WriteSummary(int numberOfExamples, int numberOfFailures, long elapsedMilliseconds)
        {
            var elapsdeTimeMessage = string.Format(
                CultureInfo.CurrentCulture, Resources.ConsoleFormatterElapsedTimeMessage, elapsedMilliseconds / 1000D);
            Console.WriteLine(elapsdeTimeMessage);

            var summaryMessage = string.Format(
                CultureInfo.CurrentCulture, Resources.ConsoleFormatterSummaryMessage, numberOfExamples, numberOfFailures);
            Console.WriteLine(summaryMessage);
        }
    }
}