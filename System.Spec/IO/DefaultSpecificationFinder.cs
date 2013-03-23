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
    using Monad.Maybe;
    using Reflection;
    using System;

    [Serializable]
    public class DefaultSpecificationFinder : ISpecificationFinder
    {
        private readonly IFileSystem fileSystem;
        
        public DefaultSpecificationFinder(IFileSystem fileSystem)
        {
            this.fileSystem = fileSystem;
        }

        public SpecificationResult GetSpecifications(string path, string example = null)
        {
            var assembly = Assembly.LoadFrom(path);
            bool foundType;

            var query = from specification in GetSpecifications(GetSpecificationTypes(assembly, example, out foundType))
                        select specification;

            return new SpecificationResult(query, foundType);
        }

        private static IEnumerable<Type> GetSpecificationTypes(Assembly assembly, string example, out bool foundType)
        {
            var exampleTypes = from exampleText in example.SomeStringOrNone()
                               from assemblyValue in assembly.SomeOrNone()
                               from exampleType in assemblyValue.GetType(exampleText).SomeOrNone()
                               where exampleType.IsSubclassOf(typeof (Specification))
                               select exampleType;
            foreach (var exampleType in exampleTypes)
            {
                foundType = true;
                return new[] {exampleType};
            }

            foundType = false;

            try {
                return from type in assembly.GetTypes() where type.IsSubclassOf(typeof(Specification)) select type;
            } catch {
                return Enumerable.Empty<Type>();
            }
        }

        public IEnumerable<string> GetSpecificationFiles(string path, string search)
        {
            var files = fileSystem.GetFilesWithExtension(GetPath(path), StringHelper.GetSearchExpression(search));

            return files;
        }

        private static IEnumerable<Specification> GetSpecifications(IEnumerable<Type> types)
        {
            return from type in types
                   select (Specification)Activator.CreateInstance(type);
        }

        private string GetPath(string path)
        {
            foreach (var pathValue in path.SomeStringOrNone()) {
                return pathValue;
            }

            return fileSystem.CurrentPath;
        }
    }
}