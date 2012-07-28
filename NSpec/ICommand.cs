namespace NSpec
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;

    public interface ICommand
    {
        IEnumerable<Assembly> Assemblies { get; }

        IEnumerable<Type> GetSpecificationTypes(Assembly assembly);

        void ExecuteSpecifications(IEnumerable<Type> types);
    }
}