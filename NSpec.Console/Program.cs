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
            IActionStrategy strategy = new DefaultActionStrategy();
            IActionStrategy exampleStratergy = arguments.DryRun
                                           ? (IActionStrategy)new NullActionStrategy()
                                           : new DefaultActionStrategy();
            ICommand command = new DefaultCommand(
                specificationVisitor, strategy, exampleStratergy, consoleFormatter, fileSystem);

            return command.ExecuteSpecificationsInPath(arguments.Example);
        }
    }
}
