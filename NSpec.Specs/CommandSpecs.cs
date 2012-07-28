namespace NSpec.Specs
{
    using System;
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
            this.specificationVisitor = new DefaultSpecificationVisitor(this.consoleFormatter);
            this.command = new DefaultCommand(this.specificationVisitor, this.consoleFormatter, this.fileSystem);
        }

        [Test]
        public void ShouldFindSpecificationsInCurrentlyRunningDll()
        {
            var types = this.command.GetSpecificationTypes(Assembly.GetExecutingAssembly());
            types.Should().HaveCount(8);
        }

        [Test]
        public void ShouldExecuteAllSpecifications()
        {
            var types = this.command.GetSpecificationTypes(Assembly.GetExecutingAssembly());

            this.command.ExecuteSpecifications(types);
            this.specificationVisitor.NumberOfExamples.Should().Be(6);
            this.specificationVisitor.NumberOfFailures.Should().Be(2);
            this.consoleFormatter.Received(4).WriteSuccess();
            this.consoleFormatter.Received(2).WriteError();
            this.consoleFormatter.Received().WriteSummary(this.specificationVisitor);
        }

        [Test]
        public void ShouldGetAssemblies()
        {
            var location = new Uri(Assembly.GetExecutingAssembly().CodeBase).LocalPath;
            const string TestPath = "test";
            this.fileSystem.CurrentPath.Returns(TestPath);
            this.fileSystem.GetFilesWithExtension(TestPath, ".dll").Returns(new[] { location });

            var assemblies = this.command.Assemblies;
            assemblies.Should().HaveCount(1);
        }
    }
}