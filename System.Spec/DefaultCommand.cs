namespace System.Spec
{
    using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Diagnostics;
	using System.Diagnostics.CodeAnalysis;
	using System.Linq;
	using System.Reflection;

	using System.Spec.Formatter;

	public class DefaultCommand : ICommand
	{
		private readonly ISpecificationVisitor visitor;
		private readonly IConsoleFormatter formatter;
		private readonly IFileSystem fileSystem;
		private readonly IActionStrategy exampleGroupStrategy;
		private readonly IActionStrategy exampleStrategy;

		public DefaultCommand(
            ISpecificationVisitor visitor,
            IActionStrategy exampleGroupStrategy,
            IActionStrategy exampleStrategy,
            IConsoleFormatter formatter,
            IFileSystem fileSystem)
		{
			this.fileSystem = fileSystem;
			this.exampleGroupStrategy = exampleGroupStrategy;
			this.exampleStrategy = exampleStrategy;
			this.visitor = visitor;
			this.formatter = formatter;
		}

		[SuppressMessage("Microsoft.Reliability", "CA2001:AvoidCallingProblematicMethods",
            MessageId = "System.Reflection.Assembly.LoadFrom", Justification = "Need to load assemblies")]
		public IEnumerable<Assembly> GetAssemblies(string path)
		{
			var files = this.fileSystem.GetFilesWithExtension(this.GetPath(path), ".dll");
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

			return from type in assembly.GetTypes() where type.IsSubclassOf(typeof(Specification)) select type;
		}

		public int ExecuteSpecifications(IEnumerable<Type> types)
		{
			var stopwatch = new Stopwatch();
			stopwatch.Start();

			foreach (var specification in types.Select(type => (Specification)Activator.CreateInstance(type))) {
				specification.Visitor = this.visitor;
				specification.ExampleGroupStrategy = this.exampleGroupStrategy;
				specification.ExampleStrategy = this.exampleStrategy;
				specification.Validate();
			}

			stopwatch.Stop();

			return this.formatter.WriteSummary(stopwatch.ElapsedMilliseconds);
		}

		public int ExecuteSpecificationsInPath(string path)
		{
			var assemblies = this.GetAssemblies(path);
			var types = new List<Type>();
			foreach (var specificationTypes in assemblies.Select(this.GetSpecificationTypes)) {
				types.AddRange(specificationTypes);
			}

			return this.ExecuteSpecifications(types);
		}

		private string GetPath(string path)
		{
			return string.IsNullOrWhiteSpace(path) ? this.fileSystem.CurrentPath : path;
		}
	}
}