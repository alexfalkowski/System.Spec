namespace System.Spec.Formatter
{
    using System.IO;

    public interface IConsoleFormatter
    {
        void WriteInformation(string message);

        void WriteSuccess(string reason, ExampleResult example);

        void WriteError(string reason, ExampleResult example);

        int WriteSummary(long elapsedMilliseconds);

        void WriteSummaryToStream(Stream stream);
    }
}