namespace System.Spec
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;

    public interface ICommand
    {
        IEnumerable<Assembly> GetAssemblies(string path, string search = StringHelper.SpecsSearch);

        IEnumerable<Type> GetSpecificationTypes(Assembly assembly);

        int ExecuteSpecifications(IEnumerable<Type> types);

        int ExecuteSpecificationsInPath(string path);
    }
}