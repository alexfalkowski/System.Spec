namespace NSpec
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Diagnostics;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Reflection;

    public class DefaultCommand : ICommand
    {
        private readonly ISpecificationVisitor visitor;

        private readonly IConsoleFormatter formatter;

        private readonly IFileSystem fileSystem;

        public DefaultCommand(ISpecificationVisitor visitor, IConsoleFormatter formatter, IFileSystem fileSystem)
        {
            this.fileSystem = fileSystem;
            this.visitor = visitor;
            this.formatter = formatter;
        }

        [SuppressMessage("Microsoft.Reliability", "CA2001:AvoidCallingProblematicMethods",
            MessageId = "System.Reflection.Assembly.LoadFile", Justification = "Need to load assemblies")]
        public IEnumerable<Assembly> GetAssemblies(string path)
        {
            var files = this.fileSystem.GetFilesWithExtension(this.GetPath(path), ".dll");
            var collection = new Collection<Assembly>();

            foreach (var file in files)
            {
                collection.Add(Assembly.LoadFile(file));
            }

            return collection;
        }

        public IEnumerable<Type> GetSpecificationTypes(Assembly assembly)
        {
            if (assembly == null)
            {
                throw new ArgumentNullException("assembly");
            }

            return from type in assembly.GetTypes() where type.IsSubclassOf(typeof(Specification)) select type;
        }

        public void ExecuteSpecifications(IEnumerable<Type> types)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            foreach (var specification in types.Select(type => (Specification)Activator.CreateInstance(type, this.visitor)))
            {
                specification.Validate();
            }

            stopwatch.Stop();

            this.formatter.WriteSummary(this.visitor, stopwatch.ElapsedMilliseconds);
        }

        private string GetPath(string path)
        {
            return string.IsNullOrWhiteSpace(path) ? this.fileSystem.CurrentPath : path;
        }
    }
}