namespace System.Spec.Formatter
{
    public class SilentConsoleFormatter : ConsoleFormatterBase
    {
        public override void WriteInformation(string message)
        {
        }

        public override int WriteSummary(long elapsedMilliseconds)
        {
            return this.ErrorResults.Count;
        }
    }
}