namespace NSpec.Formatter
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Globalization;

    using NSpec.Properties;

    public abstract class ConsoleFormatterBase : IConsoleFormatter
    {
        private readonly IList<ExampleResult> errorResults = new Collection<ExampleResult>();

        private readonly IList<ExampleResult> successResults = new Collection<ExampleResult>();

        protected IList<ExampleResult> ErrorResults
        {
            get
            {
                return this.errorResults;
            }
        }

        protected IList<ExampleResult> SuccessResults
        {
            get
            {
                return this.successResults;
            }
        }

        public abstract void WriteInformation(string message);

        public virtual void WriteSuccess(string reason, ExampleResult example)
        {
            this.SuccessResults.Add(example);
        }

        public virtual void WriteError(string reason, ExampleResult example)
        {
            this.ErrorResults.Add(example);
        }

        public virtual int WriteSummary(long elapsedMilliseconds)
        {
            var elapsdeTimeMessage = string.Format(
                CultureInfo.CurrentCulture, Resources.ConsoleFormatterElapsedTimeMessage, elapsedMilliseconds / 1000D);
            Console.WriteLine(elapsdeTimeMessage);

            var errorCount = this.ErrorResults.Count;
            var summaryMessage = string.Format(
                CultureInfo.CurrentCulture,
                Resources.ConsoleFormatterSummaryMessage,
                this.SuccessResults.Count + errorCount,
                errorCount);
            Console.WriteLine(summaryMessage);

            return errorCount;
        }
    }
}