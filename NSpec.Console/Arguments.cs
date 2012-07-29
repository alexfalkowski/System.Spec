namespace NSpec.Console
{
    using PowerArgs;

    public class Arguments
    {
        [ArgShortcut("e")]
        [ArgDescription("Execute example(s) in the specified path")]
        public string Example { get; set; }  
    }
}