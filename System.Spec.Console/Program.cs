namespace System.Spec.Console
{
    using System;

	using System.Spec.Formatter;

	using PowerArgs;

	public static class Program
	{
		public static int Main(string[] args)
		{
			try {
				var arguments = Args.Parse<Arguments>(args);

				if (arguments.Help) {
					Console.WriteLine(ArgUsage.GetUsage<Arguments>());
					return 0;
				}

				IConsoleFormatterFactory formatterFactory = new DefaultConsoleFormatterFactory();
				IConsoleFormatter consoleFormatter = formatterFactory.CreateConsoleFormatter(arguments.Format);
				ISpecificationVisitor specificationVisitor = new DefaultSpecificationVisitor(consoleFormatter);
				IFileSystem fileSystem = new DefaultFileSystem();
				IActionStrategy exampleGroupStrategy = new DefaultActionStrategy();
				IActionStrategy exampleStratergy = arguments.DryRun
                                               ? (IActionStrategy)new NullActionStrategy()
                                               : new DefaultActionStrategy();
				ICommand command = new DefaultCommand(
                    specificationVisitor, exampleGroupStrategy, exampleStratergy, consoleFormatter, fileSystem);

				return command.ExecuteSpecificationsInPath(arguments.Example);
			} catch (Exception) {
				Console.WriteLine(ArgUsage.GetUsage<Arguments>());
				return 1;
			}
		}
	}
}
