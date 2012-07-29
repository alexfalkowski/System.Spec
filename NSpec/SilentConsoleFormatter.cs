namespace NSpec
{
    using System.Collections.Generic;

    public class SilentConsoleFormatter : IConsoleFormatter
    {
        public void WriteSuccess()
        {
        }

        public void WriteError()
        {
        }

        public void WriteErrors(IEnumerable<ExampleResult> examples)
        {
        }

        public void WriteSummary(int numberOfExamples, int numberOfFailures, long elapsedMilliseconds)
        {
        }
    }
}