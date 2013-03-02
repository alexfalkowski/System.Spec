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
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Diagnostics;
    using System.Globalization;
    using System.Linq;
    using System.Reflection;

    public class DefaultSpecificationFinder : ISpecificationFinder
    {
        private readonly IFileSystem fileSystem;
        
        public DefaultSpecificationFinder(IFileSystem fileSystem)
        {
            this.fileSystem = fileSystem;
        }

        public IEnumerable<Specification> FindSpecifications(string path, string pattern, string example)
        {
            return from assembly in this.GetAssemblies(path, pattern)
                   from specification in this.GetSpecifications(this.GetSpecificationTypes(assembly, example))
                   select specification;
        }

        private IEnumerable<Specification> GetSpecifications(IEnumerable<Type> types)
        {
            return from type in types
                   select (Specification)Activator.CreateInstance(type);
        }

        private IEnumerable<Type> GetSpecificationTypes(Assembly assembly, string example)
        {
            if (assembly == null) {
                throw new ArgumentNullException("assembly");
            }

            if (!string.IsNullOrWhiteSpace(example)) {
                var exampleType = assembly.GetType(example);
                
                if (exampleType != null && exampleType.IsSubclassOf(typeof(Specification))) {
                    return new Type[] { exampleType };
                }
            }

            try {
                return from type in assembly.GetTypes() where type.IsSubclassOf(typeof(Specification)) select type;
            } catch {
                return Enumerable.Empty<Type>();
            }
        }

        private IEnumerable<Assembly> GetAssemblies(string path, string search)
        {
            var searchExpression = string.Format(CultureInfo.CurrentCulture, "{0}.dll", search);
            var files = this.fileSystem.GetFilesWithExtension(this.GetPath(path), searchExpression);

            return from file in files
                   select Assembly.LoadFrom(file);
        }

        private string GetPath(string path)
        {
            return string.IsNullOrWhiteSpace(path) ? this.fileSystem.CurrentPath : path;
        }
    }
}