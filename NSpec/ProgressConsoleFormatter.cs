namespace NSpec
{
    using System;
    using System.Globalization;

    using NSpec.Properties;

    public class ProgressConsoleFormatter : ConsoleFormatterBase
    {
        public override void WriteInformation(string message)
        {
        }

        public override void WriteSuccess(string reason, ExampleResult example)
        {
            Console.Write(Resources.ConsoleFormatterSuccessMessage);

            base.WriteSuccess(reason, example);
        }

        public override void WriteError(string reason, ExampleResult example)
        {
            Console.Write(Resources.ConsoleFormatterErrorMessage);

            base.WriteError(reason, example);
        }

        public override int WriteSummary(long elapsedMilliseconds)
        {
            var exampleResults = this.ErrorResults;

            if (exampleResults.Count > 0)
            {
                Console.WriteLine(Environment.NewLine);

                for (var index = 0; index < exampleResults.Count; index++)
                {
                    var example = exampleResults[index];
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

            return base.WriteSummary(elapsedMilliseconds);
        }
    }
}