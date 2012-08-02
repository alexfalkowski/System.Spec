namespace NSpec.Console
{
    using NSpec.Formatter;

    using PowerArgs;

    public class Arguments
    {
        [ArgShortcut("e")]
        [ArgDescription("Execute example(s) in the specified path.")]
        public string Example { get; set; }

        [ArgShortcut("f")]
        [ArgDescription("Specifies what format to use for output.")]
        public ConsoleFormatterType Format { get; set; }

        [ArgShortcut("d")]
        [ArgDescription("Invokes formatters without executing the examples.")]
        public bool DryRun { get; set; }

        [ArgShortcut("h")]
        [ArgDescription("Provides help.")]
        public bool Help { get; set; }
    }
}