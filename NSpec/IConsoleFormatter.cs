namespace NSpec
{
    public interface IConsoleFormatter
    {
        void WriteSuccess();

        void WriteError();

        void WriteSummary(ISpecificationVisitor visitor);
    }
}