namespace System.Spec.Formatter
{
    public interface IConsoleFormatterFactory
    {
        IConsoleFormatter CreateConsoleFormatter(ConsoleFormatterType type);
    }
}