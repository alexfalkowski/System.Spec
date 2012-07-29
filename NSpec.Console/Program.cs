namespace NSpec.Console
{
    using PowerArgs;

    public static class Program
    {
        public static int Main(string[] args)
        {
            var arguments = Args.Parse<Arguments>(args);
            IConsoleFormatter consoleFormatter = new DefaultConsoleFormatter();
            ISpecificationVisitor specificationVisitor = new DefaultSpecificationVisitor(consoleFormatter);
            IFileSystem fileSystem = new DefaultFileSystem();
            ICommand command = new DefaultCommand(specificationVisitor, consoleFormatter, fileSystem);

            return command.ExecuteSpecificationsInPath(arguments.Example);
        }
    }
}
