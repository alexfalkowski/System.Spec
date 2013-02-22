// Author:
//       alex.falkowski <alexrfalkowski@gmail.com>
//
//  Copyright (c) 2013 alex.falkowski
//
//  This program is free software: you can redistribute it and/or modify
//  it under the terms of the GNU Lesser General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  This program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU Lesser General Public License for more details.
//
//  You should have received a copy of the GNU Lesser General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.

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
	public class RunnerSpecs
	{
		private IRunner command;
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
			this.specificationVisitor = new DefaultSpecificationVisitor(this.consoleFormatter);
			this.command = new DefaultRunner(
                this.specificationVisitor, this.strategy, this.strategy, this.fileSystem);
		}

		[Test]
		public void ShouldFindSpecificationsInCurrentlyRunningDll()
		{
			var types = this.command.GetSpecificationTypes(Assembly.GetExecutingAssembly());
			types.Should().HaveCount(9);
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
			this.command = new DefaultRunner(
                this.specificationVisitor, this.strategy, this.strategy, new DefaultFileSystem());

			var location = Path.GetDirectoryName(new Uri(Assembly.GetExecutingAssembly().CodeBase).LocalPath);
            this.command.ExecuteSpecificationsInPath(location, StringHelper.SpecsSearch);
		}
	}
}