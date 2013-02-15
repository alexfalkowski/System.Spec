namespace System.Spec
{
    using System.Collections.Generic;
	using System.IO;
	using System.Linq;

	public class DefaultFileSystem : IFileSystem
	{
		public string CurrentPath {
			get {
				return Directory.GetCurrentDirectory();
			}
		}

		public IEnumerable<string> GetFilesWithExtension(string path, string extension)
		{
			return from file in Directory.EnumerateFiles(path, "*" + extension, SearchOption.AllDirectories) 
                   select file;
		}
	}
}