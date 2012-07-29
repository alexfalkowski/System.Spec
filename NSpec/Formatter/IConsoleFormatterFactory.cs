namespace NSpec.Formatter
{
    public interface IConsoleFormatterFactory
    {
        IConsoleFormatter CreateConsoleFormatter(ConsoleFormatterType type);
    }
}