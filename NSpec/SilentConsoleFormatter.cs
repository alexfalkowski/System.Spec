namespace NSpec
{
    public class SilentConsoleFormatter : IConsoleFormatter
    {
        public void WriteInformation(string message)
        {
        }

        public void WriteSuccess(string reason, ExampleResult example)
        {
        }

        public void WriteError(string reason, ExampleResult example)
        {
        }

        public int WriteSummary(long elapsedMilliseconds)
        {
            return 0;
        }
    }
}