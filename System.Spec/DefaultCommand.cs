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

namespace System.Spec
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Diagnostics;
    using System.Diagnostics.CodeAnalysis;
    using System.Globalization;
    using System.Linq;
    using System.Reflection;

    using System.Spec.Formatter;

    public class DefaultCommand : ICommand
    {
        private readonly ISpecificationVisitor visitor;
        private readonly IFileSystem fileSystem;
        private readonly IActionStrategy exampleGroupStrategy;
        private readonly IActionStrategy exampleStrategy;

        public DefaultCommand(
            ISpecificationVisitor visitor,
            IActionStrategy exampleGroupStrategy,
            IActionStrategy exampleStrategy,
            IFileSystem fileSystem)
        {
            this.fileSystem = fileSystem;
            this.exampleGroupStrategy = exampleGroupStrategy;
            this.exampleStrategy = exampleStrategy;
            this.visitor = visitor;
        }

        [SuppressMessage("Microsoft.Reliability", "CA2001:AvoidCallingProblematicMethods",
            MessageId = "System.Reflection.Assembly.LoadFrom", Justification = "Need to load assemblies")]
        public IEnumerable<Assembly> GetAssemblies(string path, string search)
        {
            var files = this.fileSystem.GetFilesWithExtension(this.GetPath(path), 
                                                              string.Format(CultureInfo.CurrentCulture, "{0}.dll", search));
            var collection = new Collection<Assembly>();

            foreach (var file in files) {
                collection.Add(Assembly.LoadFrom(file));
            }

            return collection;
        }

        public IEnumerable<Type> GetSpecificationTypes(Assembly assembly)
        {
            if (assembly == null) {
                throw new ArgumentNullException("assembly");
            }

            try {
                return from type in assembly.GetTypes() where type.IsSubclassOf(typeof(Specification)) select type;
            } catch {
                return Enumerable.Empty<Type>();
            }
        }

        public void ExecuteSpecifications(IEnumerable<Type> types)
        {
            foreach (var specification in types.Select(type => (Specification)Activator.CreateInstance(type))) {
                specification.Visitor = this.visitor;
                specification.ExampleGroupStrategy = this.exampleGroupStrategy;
                specification.ExampleStrategy = this.exampleStrategy;
                specification.Validate();
            }
        }

        public void ExecuteSpecificationsInPath(string path, string search)
        {
            var assemblies = this.GetAssemblies(path, search);
            var types = new List<Type>();

            foreach (var specificationTypes in assemblies.Select(this.GetSpecificationTypes)) {
                types.AddRange(specificationTypes);
            }

            this.ExecuteSpecifications(types);
        }

        private string GetPath(string path)
        {
            return string.IsNullOrWhiteSpace(path) ? this.fileSystem.CurrentPath : path;
        }
    }
}