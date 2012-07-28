namespace NSpec
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;

    public interface ICommand
    {
        IEnumerable<Assembly> GetAssemblies(string path);

        IEnumerable<Type> GetSpecificationTypes(Assembly assembly);

        void ExecuteSpecifications(IEnumerable<Type> types);
    }
}