namespace NSpec
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    public class Command
    {
        public IEnumerable<Type> GetSpecificationTypes(Assembly assembly)
        {
            return from type in assembly.GetTypes() where type.IsSubclassOf(typeof(Specification)) select type;
        }
    }
}