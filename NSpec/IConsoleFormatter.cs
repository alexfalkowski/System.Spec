namespace NSpec
{
    using System.Collections.Generic;

    public interface IConsoleFormatter
    {
        void WriteSuccess();

        void WriteError();

        void WriteErrors(IEnumerable<ExampleResult> examples);

        void WriteSummary(int numberOfExamples, int numberOfFailures, long elapsedMilliseconds);
    }
}