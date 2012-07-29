namespace NSpec.Specs
{
    using System;
    using System.IO;
    using System.Reflection;

    using FluentAssertions;

    using NSubstitute;

    using NUnit.Framework;

    [TestFixture]
    public class CommandSpecs
    {
        private ICommand command;

        private ISpecificationVisitor specificationVisitor;

        private IConsoleFormatter consoleFormatter;

        private IFileSystem fileSystem;

        [SetUp]
        public void BeforeEach()
        {
            this.fileSystem = Substitute.For<IFileSystem>();
            this.consoleFormatter = Substitute.For<IConsoleFormatter>();
            this.consoleFormatter.WriteSummary(Arg.Any<long>()).Returns(2);
            this.specificationVisitor = new DefaultSpecificationVisitor(this.consoleFormatter);
            this.command = new DefaultCommand(this.specificationVisitor, this.consoleFormatter, this.fileSystem);
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
            this.consoleFormatter.Received(7).WriteSuccess(Arg.Any<string>(), Arg.Any<ExampleResult>());
            this.consoleFormatter.Received(2).WriteError(Arg.Any<string>(), Arg.Any<ExampleResult>());
            this.consoleFormatter.Received().WriteSummary(Arg.Any<long>());
        }

        [Test]
        public void ShouldGetAssemblies()
        {
            var location = new Uri(Assembly.GetExecutingAssembly().CodeBase).LocalPath;
            const string TestPath = "test";
            this.fileSystem.CurrentPath.Returns(TestPath);
            this.fileSystem.GetFilesWithExtension(TestPath, ".dll").Returns(new[] { location });

            var assemblies = this.command.GetAssemblies(TestPath);
            assemblies.Should().HaveCount(1);
        }

        [Test]
        public void ShouldExecuteAllSpecificationsInPath()
        {
            this.command = new DefaultCommand(this.specificationVisitor, this.consoleFormatter, new DefaultFileSystem());

            var location = Path.GetDirectoryName(new Uri(Assembly.GetExecutingAssembly().CodeBase).LocalPath);
            var result = this.command.ExecuteSpecificationsInPath(location);
            result.Should().Be(2);
        }
    }
}