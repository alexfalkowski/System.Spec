namespace System.Spec.Specs
{
    using System;
    using System.IO;
    using System.Reflection;

    using FluentAssertions;

    using NUnit.Framework;

    [TestFixture]
    public class FileSystemSpecs
    {
        private IFileSystem fileSystem;

        [SetUp]
        public void BeforeEach()
        {
            this.fileSystem = new DefaultFileSystem();
        }

        [Test]
        public void ShouldFindDlls()
        {
            var location = Path.GetDirectoryName(new Uri(Assembly.GetExecutingAssembly().CodeBase).LocalPath);
            var files = this.fileSystem.GetFilesWithExtension(location, ".dll");
            files.Should().NotBeEmpty();
        }

        [Test]
        public void ShouldGetCurrentPath()
        {
            var currentPath = this.fileSystem.CurrentPath;
            currentPath.Should().Contain("NSpec");
        }
    }
}