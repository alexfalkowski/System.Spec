namespace NSpec
{
    public interface IConsoleFormatterFactory
    {
        IConsoleFormatter CreateConsoleFormatter(ConsoleFormatterType type);
    }
}