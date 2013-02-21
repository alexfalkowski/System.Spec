namespace System.Spec.Specs
{
    using System;
	using System.IO;
	using System.Reflection;
    using System.Xml;
    using System.Xml.Serialization;

	using FluentAssertions;

	using System.Spec.Formatter;

	using NSubstitute;

	using NUnit.Framework;

	[TestFixture]
	public class CommandSpecs
	{
		private ICommand command;
		private ISpecificationVisitor specificationVisitor;
		private IConsoleFormatter consoleFormatter;
		private IFileSystem fileSystem;
		private IActionStrategy strategy;

		[SetUp]
		public void BeforeEach()
		{
			this.fileSystem = Substitute.For<IFileSystem>();
			this.strategy = new DefaultActionStrategy();
			this.consoleFormatter = Substitute.For<IConsoleFormatter>();
			this.consoleFormatter.WriteSummary(Arg.Any<long>()).Returns(2);
			this.specificationVisitor = new DefaultSpecificationVisitor(this.consoleFormatter);
			this.command = new DefaultCommand(
                this.specificationVisitor, this.strategy, this.strategy, this.consoleFormatter, this.fileSystem);
		}

		[Test]
		public void ShouldFindSpecificationsInCurrentlyRunningDll()
		{
			var types = this.command.GetSpecificationTypes(Assembly.GetExecutingAssembly());
			types.Should().HaveCount(9);
		}

		[Test]
		public void ShouldExecuteAllSpecifications()
		{
			var types = this.command.GetSpecificationTypes(Assembly.GetExecutingAssembly());
			var result = this.command.ExecuteSpecifications(types);

			result.Should().Be(2);
			this.consoleFormatter.Received(5).WriteSuccess(Arg.Any<string>(), Arg.Any<ExampleResult>());
			this.consoleFormatter.Received(2).WriteError(Arg.Any<string>(), Arg.Any<ExampleResult>());
			this.consoleFormatter.Received().WriteSummary(Arg.Any<long>());
		}

		[Test]
		public void ShouldGetAssembliesWithDefaultSearch()
		{
			var location = new Uri(Assembly.GetExecutingAssembly().CodeBase).LocalPath;
			const string TestPath = "test";
			this.fileSystem.CurrentPath.Returns(TestPath);
			this.fileSystem.GetFilesWithExtension(TestPath, "Specs.dll").Returns(new[] { location });

            var assemblies = this.command.GetAssemblies(TestPath, StringHelper.SpecsSearch);
			assemblies.Should().HaveCount(1);
		}

        [Test]
        public void ShouldGetAssembliesWithCustomSearch()
        {
            var location = new Uri(Assembly.GetExecutingAssembly().CodeBase).LocalPath;
            const string TestPath = "test";
            this.fileSystem.CurrentPath.Returns(TestPath);
            this.fileSystem.GetFilesWithExtension(TestPath, "Spec.Specs.dll").Returns(new[] { location });
            
            var assemblies = this.command.GetAssemblies(TestPath, "Spec.Specs");
            assemblies.Should().HaveCount(1);
        }

		[Test]
		public void ShouldExecuteAllSpecificationsInPath()
		{
			this.command = new DefaultCommand(
                this.specificationVisitor, this.strategy, this.strategy, this.consoleFormatter, new DefaultFileSystem());

			var location = Path.GetDirectoryName(new Uri(Assembly.GetExecutingAssembly().CodeBase).LocalPath);
            var result = this.command.ExecuteSpecificationsInPath(location, StringHelper.SpecsSearch);
			result.Should().Be(2);
		}
	}
}