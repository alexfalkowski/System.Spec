namespace NSpec.Console
{
    using PowerArgs;

    public class Arguments
    {
        [ArgShortcut("e")]
        [ArgDescription("Execute example(s) in the specified path.")]
        public string Example { get; set; }

        [ArgShortcut("f")]
        [ArgDescription("Specifies what format to use for output.")]
        public ConsoleFormatterType Format { get; set; }
    }
}