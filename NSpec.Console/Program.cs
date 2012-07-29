namespace NSpec.Console
{
    using NSpec.Formatter;

    using PowerArgs;

    public static class Program
    {
        public static int Main(string[] args)
        {
            var arguments = Args.Parse<Arguments>(args);
            IConsoleFormatterFactory formatterFactory = new DefaultConsoleFormatterFactory();
            IConsoleFormatter consoleFormatter = formatterFactory.CreateConsoleFormatter(arguments.Format);
            ISpecificationVisitor specificationVisitor = new DefaultSpecificationVisitor(consoleFormatter);
            IFileSystem fileSystem = new DefaultFileSystem();
            ICommand command = new DefaultCommand(specificationVisitor, consoleFormatter, fileSystem);

            return command.ExecuteSpecificationsInPath(arguments.Example);
        }
    }
}
