namespace NSpec.Console
{
    using PowerArgs;

    // ReSharper disable ClassNeverInstantiated.Global
    public class Arguments
    // ReSharper restore ClassNeverInstantiated.Global
    {
        [ArgShortcut("e")]
        [ArgDescription("Execute example(s) in the specified path.")]
        // ReSharper disable UnusedAutoPropertyAccessor.Global
        public string Example { get; set; }
        // ReSharper restore UnusedAutoPropertyAccessor.Global
        [ArgShortcut("f")]
        [ArgDescription("Specifies what format to use for output.")]
        // ReSharper disable UnusedAutoPropertyAccessor.Global
        public ConsoleFormatterType Format { get; set; }
        // ReSharper restore UnusedAutoPropertyAccessor.Global
    }
}