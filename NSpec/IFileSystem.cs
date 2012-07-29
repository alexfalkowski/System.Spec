namespace NSpec
{
    using System.Collections.Generic;

    public interface IFileSystem
    {
        string CurrentPath { get; }

        IEnumerable<string> GetFilesWithExtension(string path, string extension);
    }
}