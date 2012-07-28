namespace NSpec
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
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
            MessageId = "System.Reflection.Assembly.LoadFile", 
            Justification = "Need to load assemblies")]
        public IEnumerable<Assembly> Assemblies
        {
            get
            {
                var files = this.fileSystem.GetFilesWithExtension(this.fileSystem.CurrentPath, ".dll");
                var collection = new Collection<Assembly>();

                foreach (var file in files)
                {
                    collection.Add(Assembly.LoadFile(file));
                }

                return collection;
            }
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
            foreach (var specification in types.Select(type => (Specification)Activator.CreateInstance(type, this.visitor)))
            {
                specification.Validate();
            }

            this.formatter.WriteSummary(this.visitor);
        }
    }
}