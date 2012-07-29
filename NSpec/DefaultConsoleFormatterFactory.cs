namespace NSpec
{
    public class DefaultConsoleFormatterFactory : IConsoleFormatterFactory
    {
        public IConsoleFormatter CreateConsoleFormatter(ConsoleFormatterType type)
        {
            if (type == ConsoleFormatterType.Progress)
            {
                return new ProgressConsoleFormatter();
            }

            return new SilentConsoleFormatter();
        }
    }
}