namespace System.Spec
{
    using System.Collections.Generic;
    using System.IO;

    public interface IFileSystem
    {
        string CurrentPath { get; }

        IEnumerable<string> GetFilesWithExtension(string path, string extension);

        FileStream OpenWrite(string path);
    }
}