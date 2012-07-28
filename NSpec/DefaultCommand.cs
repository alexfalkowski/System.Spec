namespace NSpec
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    public class DefaultCommand : ICommand
    {
        private readonly ISpecificationVisitor visitor;

        private readonly IConsoleFormatter formatter;

        public DefaultCommand(ISpecificationVisitor visitor, IConsoleFormatter formatter)
        {
            this.visitor = visitor;
            this.formatter = formatter;
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