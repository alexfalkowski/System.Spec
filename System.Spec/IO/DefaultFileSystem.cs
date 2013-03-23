// Author:
//       alex.falkowski <alexrfalkowski@gmail.com>
//
//  Copyright (c) 2013 alex.falkowski
//
//  This program is free software: you can redistribute it and/or modify
//  it under the terms of the GNU Lesser General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  This program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU Lesser General Public License for more details.
//
//  You should have received a copy of the GNU Lesser General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.

namespace System.Spec.IO
{
    using Collections.Generic;
    using Linq;
    using System.IO;

    [Serializable]
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

        public FileStream OpenWrite(string path)
        {
            return File.Open(path, FileMode.Create);
        }
    }
}