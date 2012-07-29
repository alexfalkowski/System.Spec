namespace NSpec.Formatter
{
    public class DefaultConsoleFormatterFactory : IConsoleFormatterFactory
    {
        public IConsoleFormatter CreateConsoleFormatter(ConsoleFormatterType type)
        {
            if (type == ConsoleFormatterType.Progress)
            {
                return new ProgressConsoleFormatter();
            }

            if (type == ConsoleFormatterType.Nested)
            {
                return new NestedConsoleFormatter();
            }

            return new SilentConsoleFormatter();
        }
    }
}